using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    /// <summary>
    /// Service layer for Adopter entity that handles business logic.
    /// </summary>
    public class AdopterService : IAdopterService
    {
        private IAdopterRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdopterService"/> class.
        /// </summary>
        /// <param name="repository">Adopter repository interface.</param>
        public AdopterService(IAdopterRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Retrieves all adopters from the repository.
        /// </summary>
        public async Task<IEnumerable<Adopter>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a specific adopter by ID.
        /// </summary>
        public async Task<Adopter> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new adopter to the repository.
        /// </summary>
        public async Task<Adopter> AddAsync(Adopter adopter)
        {
            return await _repo.AddAsync(adopter);
        }

        /// <summary>
        /// Updates an existing adopter's data.
        /// </summary>
        /// <param name="id">Adopter ID to update.</param>
        /// <param name="updatedAdopter">New adopter data.</param>
        public async Task<Adopter> UpdateAsync(int id, Adopter updatedAdopter)
        {
            updatedAdopter.Id = id;
            return await _repo.UpdateAsync(updatedAdopter);
        }

        /// <summary>
        /// Deletes an adopter by their ID.
        /// </summary>
        public async Task<Adopter> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
