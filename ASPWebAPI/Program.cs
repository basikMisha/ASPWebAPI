using ASPWebAPI.Api.Mapping;
using ASPWebAPI.BLL;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using ASPWebAPI.Api.Validators.Adopter;
using ASPWebAPI.BLL.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Text.Json;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();


try
{
    Log.Information("Starting up the application");

    var builder = WebApplication.CreateBuilder(args);

    // Serilog
    builder.Host.UseSerilog();

    //Controllers
    builder.Services.AddControllers();

    //BLL
    builder.Services.AddBLLServices(builder.Configuration);

    //Database connection
    builder.Services.AddScoped<IDbConnection>(sp =>
        new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

    //Health check
    builder.Services.AddHealthChecks()
        .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    //AutoMapper
    builder.Services.AddAutoMapper(typeof(AdopterProfile).Assembly);

    //Validation
    builder.Services.AddValidatorsFromAssemblyContaining<CreateAdopterDtoValidator>();
    builder.Services.AddFluentValidationAutoValidation();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        // Jwt
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGci...\""
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    });

    //Jwt
    //secrets configuration
    builder.Configuration.AddUserSecrets<Program>();

    //JwtSettings from configuration
    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
    if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
    {
        throw new Exception("JWT Key is not set. Use user-secrets: 'dotnet user-secrets set \"Jwt:Key\" \"your_key\"'");
    }

    //Authentication configuration
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key))
        };
    });
    builder.Services.AddSingleton(jwtSettings);

    var app = builder.Build();

    //Seed Admin
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        await ASPWebAPI.Api.Infrastructure.DatabaseSeeder.SeedAdminUserAsync(services, logger);
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Health endpoint
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var json = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new {
                    component = e.Key,
                    status = e.Value.Status.ToString()
                })
            });
            await context.Response.WriteAsync(json);
        }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}