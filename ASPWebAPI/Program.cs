using ASPWebAPI.Api.Mapping;
using ASPWebAPI.BLL;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using ASPWebAPI.Api.Validators.Adopter;
using ASPWebAPI.DAL;

var builder = WebApplication.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();

//DAL
builder.Services.AddDALServices();

//BLL
builder.Services.AddBLLServices();

//Database connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
