using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Dapper;
using System.Data;

namespace ASPWebAPI.DAL.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly IDbConnection _db;

        public VolunteerRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Volunteer>> GetAllAsync()
        {
            var sql = "SELECT * FROM roles.Volunteer";
            return await _db.QueryAsync<Volunteer>(sql);
        }

        public async Task<Volunteer> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM roles.Volunteer WHERE Id = @id";
            return await _db.QuerySingleOrDefaultAsync<Volunteer>(sql, new { Id = id });
        }

        public async Task<Volunteer> AddAsync(Volunteer volunteer)
        {
            var sql = @"INSERT INTO roles.Volunteer (Name, Role, StartDate, Email)
                      VALUES (@Name, @Role, @StartDate, @Email);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _db.ExecuteScalarAsync<int>(sql, volunteer);
            volunteer.Id = id;
            return volunteer;
        }

        public async Task<Volunteer> UpdateAsync(Volunteer volunteer)
        {
            var existing = await GetByIdAsync(volunteer.Id);

            if (existing == null) return null;

            var sql = @"UPDATE roles.Volunteer
                      SET Name = @Name,
                      Role = @Role,
                      StartDate = StartDate,
                      Email = @Email
                      WHERE Id = @Id";
            await _db.ExecuteAsync(sql, volunteer);
            return volunteer;
        }

        public async Task<Volunteer> DeleteByIdAsync(int id)
        {
            var volunteer = await GetByIdAsync(id);

            if (volunteer == null) return null;

            var sql = "DELETE FROM roles.Volunteer WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });

            return volunteer;
        }
    }
}
