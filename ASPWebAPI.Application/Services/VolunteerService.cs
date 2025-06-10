using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    /// <summary>
    /// Service for managing volunteer-related operations.
    /// </summary>
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _repo;

        public VolunteerService(IVolunteerRepository repo) 
        { 
            _repo = repo;
        }

        /// <summary>
        /// Retrieves all volunteers.
        /// </summary>
        public async Task<IEnumerable<Volunteer>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        /// <summary>
        /// Retrieves a volunteer by ID.
        /// </summary>
        public async Task<Volunteer> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        /// <summary>
        /// Adds a new volunteer.
        /// </summary>
        public async Task<Volunteer> AddAsync(Volunteer volunteer)
        {
            return await _repo.AddAsync(volunteer);
        }

        /// <summary>
        /// Updates an existing volunteer by ID.
        /// </summary>
        public async Task<Volunteer> UpdateAsync(int id, Volunteer updatedVolunteer)
        {
            var existingVolunteer = await _repo.GetByIdAsync(id);

            if (existingVolunteer == null) return null;
            
            existingVolunteer.Name = updatedVolunteer.Name;
            existingVolunteer.Role = updatedVolunteer.Role;
            existingVolunteer.StartDate = updatedVolunteer.StartDate;
            existingVolunteer.Email = updatedVolunteer.Email;

            return await _repo.UpdateAsync(existingVolunteer);
        }

        /// <summary>
        /// Deletes a volunteer by ID.
        /// </summary>
        public async Task<Volunteer> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
