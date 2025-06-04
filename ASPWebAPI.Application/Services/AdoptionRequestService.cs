using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AdoptionRequestService : IAdoptionRequestService
    {
        private IAdoptionRequestRepository _repo;

        public AdoptionRequestService(IAdoptionRequestRepository repo)
        {
            _repo = repo;
        }   

        public async Task<IEnumerable<AdoptionRequest>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<AdoptionRequest> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<AdoptionRequest> AddAsync(AdoptionRequest adoptionRequest)
        {
            return await _repo.AddAsync(adoptionRequest);
        }

        public async Task<AdoptionRequest> UpdateAsync(int id, AdoptionRequest updatedAdoptionRequest)
        {
            updatedAdoptionRequest.Id = id;
            return await _repo.UpdateAsync(updatedAdoptionRequest);
        }

        public async Task<AdoptionRequest> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
