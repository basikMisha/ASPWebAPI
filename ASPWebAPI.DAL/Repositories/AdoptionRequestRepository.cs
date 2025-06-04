using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Dapper;
using System.Data;


namespace ASPWebAPI.DAL.Repositories
{
    public class AdoptionRequestRepository : IAdoptionRequestRepository
    {
        private readonly IDbConnection _db;

        public AdoptionRequestRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<AdoptionRequest>> GetAllAsync()
        {
            var sql = "SELECT * FROM adoption.AdoptionRequest";
            return await _db.QueryAsync<AdoptionRequest>(sql);
        }

        public async Task<AdoptionRequest> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM adoption.AdoptionRequest WHERE Id = @id";
            return await _db.QuerySingleOrDefaultAsync<AdoptionRequest>(sql, new { Id = id });
        }

        public async Task<AdoptionRequest> AddAsync(AdoptionRequest adoptionRequest)
        {
            var sql = @"INSERT INTO adoption.AdoptionRequest (PetId, AdopterId, RequestDate, AdoptionDate, Status)
                      VALUES(@PetId, @AdopterId, @RequestDate, @AdoptionDate, @Status);
                      SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _db.ExecuteScalarAsync<int>(sql, adoptionRequest);
            adoptionRequest.Id = id;
            return adoptionRequest;
        }

        public async Task<AdoptionRequest> UpdateAsync(AdoptionRequest adoptionRequest)
        {
            var existing = await GetByIdAsync(adoptionRequest.Id);

            if (existing == null) return null;

            var sql = @"UPDATE adoption.AdoptionRequest
                      SET PetId = @PetId,
                      AdopterId = @AdopterId,
                      RequestDate = @RequestDate,
                      AdoptionDate = @AdoptionDate,
                      Status = @Status
                      WHERE Id = @Id";
            await _db.ExecuteAsync(sql, adoptionRequest);
            return adoptionRequest;
        }

        public async Task<AdoptionRequest> DeleteByIdAsync(int id)
        {
            var adoptionRequest = await GetByIdAsync(id);

            if (adoptionRequest == null) return null;

            var sql = "DELETE FROM adoption.AdoptionRequest WHERE Id = @Id";
            await _db.ExecuteAsync(sql, new { Id = id });
            
            return adoptionRequest;
        }
    }
}
