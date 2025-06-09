using ASPWebAPI.Domain.Interfaces;
using ASPWebAPI.Domain.Entities;
using System.Data;
using Dapper;

namespace ASPWebAPI.DAL.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly IDbConnection _db;

        public PetRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var sql = "SELECT COUNT(1) FROM adoption.Pet WHERE Id = @Id";
            return await _db.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            var sql = "SELECT * FROM adoption.Pet";
            return await _db.QueryAsync<Pet>(sql);
        }

        public async Task<Pet> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM adoption.Pet WHERE Id = @id";
            return await _db.QuerySingleOrDefaultAsync<Pet>(sql, new { Id = id });
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            var sql = @"INSERT INTO adoption.Pet(Name, Species, Age, IsAdopted, PhotoUrl, Description, VolunteerId)
                      VALUES (@Name, @Species, @Age, @IsAdopted, @PhotoUrl, @Description, @VolunteerId);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _db.ExecuteScalarAsync<int>(sql, pet);
            pet.Id = id;
            return pet;
        }

        public async Task<Pet> UpdateAsync(Pet pet)
        {
            var existing = await GetByIdAsync(pet.Id);

            if (existing == null) return null;

            var sql = @"UPDATE adoption.Pet
                      SET Name = @Name,
                      Species = @Species,
                      Age = @Age,
                      IsAdopted = @IsAdopted,
                      PhotoUrl = @PhotoUrl,
                      Description = @Description,
                      VolunteerId = @VolunteerId
                      WHERE Id = @Id";
            await _db.ExecuteAsync(sql, pet);
            return pet;
        }

        public async Task<Pet> DeleteByIdAsync(int id)
        {
            var pet = await GetByIdAsync(id);

            if (pet == null) return null;

            var sql = "DELETE FROM adoption.Pet WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });

            return pet;
        }
    }
}
