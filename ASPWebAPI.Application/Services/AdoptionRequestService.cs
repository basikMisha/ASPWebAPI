using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AdoptionRequestService : IAdoptionRequestService
    {
        private IAdoptionRequestRepository _repo;
        private readonly IPetRepository _petRepo;
        private readonly IAdopterRepository _adopterRepo;

        public AdoptionRequestService(
        IAdoptionRequestRepository repo,
        IPetRepository petRepo,
        IAdopterRepository adopterRepo)
        {
            _repo = repo;
            _petRepo = petRepo;
            _adopterRepo = adopterRepo;
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
            if (!await _petRepo.ExistsAsync(adoptionRequest.PetId))
                throw new ArgumentException($"Pet with ID {adoptionRequest.PetId} does not exist.");

            if (!await _adopterRepo.ExistsAsync(adoptionRequest.AdopterId))
                throw new ArgumentException($"Adopter with ID {adoptionRequest.AdopterId} does not exist.");

            return await _repo.AddAsync(adoptionRequest);
        }

        public async Task<AdoptionRequest> UpdateAsync(int id, AdoptionRequest updatedAdoptionRequest)
        {
            if (!await _petRepo.ExistsAsync(updatedAdoptionRequest.PetId))
                throw new ArgumentException($"Pet with ID {updatedAdoptionRequest.PetId} does not exist.");

            if (!await _adopterRepo.ExistsAsync(updatedAdoptionRequest.AdopterId))
                throw new ArgumentException($"Adopter with ID {updatedAdoptionRequest.AdopterId} does not exist.");

            updatedAdoptionRequest.Id = id;
            return await _repo.UpdateAsync(updatedAdoptionRequest);
        }

        public async Task<AdoptionRequest> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
