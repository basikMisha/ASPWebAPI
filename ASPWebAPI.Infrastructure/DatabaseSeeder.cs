using System.Data;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPWebAPI.Api.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider services, ILogger logger)
        {
            var db = services.GetRequiredService<IDbConnection>();

            const string checkAdminSql = "SELECT COUNT(*) FROM auth.[User] WHERE Role = 'Admin'";
            var count = await db.ExecuteScalarAsync<int>(checkAdminSql);

            if (count == 0)
            {
                var email = "admin@example.com";
                var password = "Admin123!";
                var hash = BCrypt.Net.BCrypt.HashPassword(password);

                const string insertSql = @"
                    INSERT INTO auth.[User] (Email, PasswordHash, Role)
                    VALUES (@Email, @PasswordHash, @Role)";

                await db.ExecuteAsync(insertSql, new
                {
                    Email = email,
                    PasswordHash = hash,
                    Role = "Admin"
                });

                logger.LogInformation("Admin user created: {Email} / {Password}", email, password);
            }
            else
            {
                logger.LogInformation("Admin user already exists");
            }
        }

    }

}
