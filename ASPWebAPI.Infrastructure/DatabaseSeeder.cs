using System.Data;
using Dapper;
using Microsoft.Extensions.DependencyInjection;

namespace ASPWebAPI.Api.Infrastructure
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider services)
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

                Console.WriteLine("Admin created: admin@example.com / Admin123");
            }
            else
            {
                Console.WriteLine("Admin already exists");
            }
        }
    }

}
