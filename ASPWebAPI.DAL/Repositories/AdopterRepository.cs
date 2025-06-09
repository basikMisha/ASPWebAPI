using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using System.Data;
using Dapper;

namespace ASPWebAPI.DAL.Repositories
{
    public class AdopterRepository : IAdopterRepository
    {
        private readonly IDbConnection _db;

        public AdopterRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var sql = "SELECT COUNT(1) FROM roles.Adopter WHERE Id = @Id";
            return await _db.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Adopter>> GetAllAsync()
        {
            var sql = "SELECT * FROM roles.Adopter";
            return await _db.QueryAsync<Adopter>(sql);
        }

        public async Task<Adopter> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM roles.Adopter WHERE Id = @id";
            return await _db.QuerySingleOrDefaultAsync<Adopter>(sql, new { Id = id });
        }

        public async Task<Adopter> AddAsync(Adopter adopter)
        {
            var sql = @"INSERT INTO roles.Adopter (Name, Email, Phone)
                      VALUES (@Name, @Email, @Phone);
                      SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = await _db.ExecuteScalarAsync<int>(sql, adopter);
            adopter.Id = id;
            return adopter;
        }

        public async Task<Adopter> UpdateAsync(Adopter adopter)
        {
            var existing = await GetByIdAsync(adopter.Id);

            if (existing == null) return null;

            var sql = @"UPDATE roles.Adopter
                      SET Name = @Name,
                      Email = @Email,
                      Phone = @Phone
                      WHERE Id = @Id";
            await _db.ExecuteAsync(sql, adopter);
            return adopter;
        }


        public async Task<Adopter> DeleteByIdAsync(int id)
        {
            var adopter = await GetByIdAsync(id);

            if (adopter == null) return null;

            var sql = "DELETE FROM roles.Adopter WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });

            return adopter;
        }
    }
}
