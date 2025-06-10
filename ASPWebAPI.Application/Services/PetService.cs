using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    /// <summary>
    /// Service class that handles business logic related to Pet entities.
    /// </summary>
    public class PetService : IPetService
    {
        private readonly IPetRepository _repo;
        private readonly IVolunteerRepository _volunteerRepo;

        /// <summary>
        /// Initializes a new instance of <see cref="PetService"/> with specified repositories.
        /// </summary>
        public PetService(IPetRepository repo, IVolunteerRepository volunteerRepo)
        {
            _repo = repo;
            _volunteerRepo = volunteerRepo;
        }

        /// <summary>
        /// Retrieves all pets.
        /// </summary>
        public async Task<IEnumerable<Pet>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a pet by its ID.
        /// </summary>
        public async Task<Pet> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new pet, verifying if the associated volunteer exists.
        /// </summary>
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

        /// <summary>
        /// Updates an existing pet after verifying volunteer existence.
        /// </summary>
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

        /// <summary>
        /// Deletes a pet by its ID.
        /// </summary>
        public async Task<Pet> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
