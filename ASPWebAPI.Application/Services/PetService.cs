using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;
        private readonly IVolunteerRepository _volunteerRepo;

        public PetService(IPetRepository repo, IVolunteerRepository volunteerRepo)
        {
            _repo = repo;
            _volunteerRepo = volunteerRepo;
        }

        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Pet> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Pet> AddAsync(Pet pet)
        {
            if (pet.VolunteerId.HasValue)
            {
                var exists = await _volunteerRepo.ExistsAsync(pet.VolunteerId.Value);
                if (!exists)
                    throw new ArgumentException($"Volunteer with ID {pet.VolunteerId} does not exist.");
            }

            return await _repo.AddAsync(pet);
        }

        public async Task<Pet> UpdateAsync(int id, Pet updatedPet)
        {
            if (updatedPet.VolunteerId.HasValue)
            {
                var exists = await _volunteerRepo.ExistsAsync(updatedPet.VolunteerId.Value);
                if (!exists)
                    throw new ArgumentException($"Volunteer with ID {updatedPet.VolunteerId} does not exist.");
            }

            updatedPet.Id = id;
            return await _repo.UpdateAsync(updatedPet);
        }

        public async Task<Pet> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
