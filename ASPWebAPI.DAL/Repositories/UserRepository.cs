using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Dapper;
using System.Data;
using System.Data.Common;

namespace ASPWebAPI.DAL.Dapper.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM auth.[User]";
            return await _db.QueryAsync<User>(sql);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM auth.[User] WHERE Id = @id";
            return await _db.QueryFirstOrDefaultAsync<User>(sql, new {Id = id});
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM auth.[User] WHERE Email = @email";
            return await _db.QueryFirstOrDefaultAsync<User>(sql, new {Email = email});
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            var sql = @"INSERT INTO auth.[User] (Email, PasswordHash, Role)
                      VALUES (@Email, @PasswordHash, @Role);
                      SELECT CAST(SCOPE_IDENTITY() as int)";

            return await _db.QuerySingleAsync<User>(sql, new { Email = email, PasswordHash = password, Role = "User"});
        }

        public async Task<User> AddAsync(User user)
        {
            var sql = @"INSERT INTO auth.[User] (Email, PasswordHash, Role)
                      VALUES (@Email, @PasswordHash, @Role);
                      SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await _db.ExecuteScalarAsync<int>(sql, user);
            user.Id = id;
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var existing = await GetByIdAsync(user.Id);
            if (existing == null) return null;

            var sql = @"UPDATE auth.[User]
                      SET Email = @Email,
                      PasswordHash = @PasswordHash,
                      Role = @Role
                      WHERE Id = @Id";
            await _db.ExecuteAsync(sql, user);
            return user;
        }

        public async Task<User> DeleteByIdAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user == null) return null;

            var sql = "DELETE FROM auth.[User] WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });

            return user;
        }
    }
}
