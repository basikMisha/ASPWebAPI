using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASPWebAPI.BLL.Services
{
    /// <summary>
    /// Service layer for managing adoption requests.
    /// </summary>
    public class AdoptionRequestService : IAdoptionRequestService
    {
        private readonly IAdoptionRequestRepository _adoptionRequestRepository;
        private readonly IPetRepository _petRepository;
        private readonly IAdopterRepository _adopterRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdoptionRequestService"/> class.
        /// </summary>
        public AdoptionRequestService(
            IAdoptionRequestRepository adoptionRequestRepository,
            IPetRepository petRepository,
            IAdopterRepository adopterRepository)
        {
            _adoptionRequestRepository = adoptionRequestRepository;
            _petRepository = petRepository;
            _adopterRepository = adopterRepository;
        }

        /// <summary>
        /// Retrieves all adoption requests.
        /// </summary>
        public async Task<IEnumerable<AdoptionRequest>> GetAllAsync()
        {
            return await _adoptionRequestRepository.GetAllAsync();
        }

        /// <summary>
        /// Retrieves an adoption request by its ID.
        /// </summary>
        public async Task<AdoptionRequest> GetByIdAsync(int id)
        {
            return await _adoptionRequestRepository.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new adoption request after validating referenced entities.
        /// </summary>
        /// <param name="adoptionRequest">Adoption request to add.</param>
        /// <exception cref="ArgumentException">Thrown when referenced Pet or Adopter does not exist.</exception>
        public async Task<AdoptionRequest> AddAsync(AdoptionRequest adoptionRequest)
        {
            await ValidateRelatedEntitiesExistAsync(adoptionRequest.PetId, adoptionRequest.AdopterId);
            return await _adoptionRequestRepository.AddAsync(adoptionRequest);
        }

        /// <summary>
        /// Updates an existing adoption request after validating referenced entities.
        /// </summary>
        /// <param name="id">ID of the adoption request to update.</param>
        /// <param name="updatedAdoptionRequest">Updated adoption request data.</param>
        /// <exception cref="ArgumentException">Thrown when referenced Pet or Adopter does not exist.</exception>
        public async Task<AdoptionRequest> UpdateAsync(int id, AdoptionRequest updatedAdoptionRequest)
        {
            await ValidateRelatedEntitiesExistAsync(updatedAdoptionRequest.PetId, updatedAdoptionRequest.AdopterId);

            updatedAdoptionRequest.Id = id;
            return await _adoptionRequestRepository.UpdateAsync(updatedAdoptionRequest);
        }

        /// <summary>
        /// Deletes an adoption request by ID.
        /// </summary>
        public async Task<AdoptionRequest> DeleteByIdAsync(int id)
        {
            return await _adoptionRequestRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Validates that Pet and Adopter with specified IDs exist.
        /// </summary>
        /// <param name="petId">Pet ID.</param>
        /// <param name="adopterId">Adopter ID.</param>
        /// <exception cref="ArgumentException">Thrown if any entity does not exist.</exception>
        private async Task ValidateRelatedEntitiesExistAsync(int petId, int adopterId)
        {
            if (!await _petRepository.ExistsAsync(petId))
                throw new ArgumentException($"Pet with ID {petId} does not exist.");

            if (!await _adopterRepository.ExistsAsync(adopterId))
                throw new ArgumentException($"Adopter with ID {adopterId} does not exist.");
        }
    }
}
