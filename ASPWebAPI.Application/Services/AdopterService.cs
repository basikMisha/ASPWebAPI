using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AdopterService : IAdopterService
    {
        private IAdopterRepository _repo;

        public AdopterService(IAdopterRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Adopter>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Adopter> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Adopter> AddAsync(Adopter adopter)
        {
            return await _repo.AddAsync(adopter);
        }

        public async Task<Adopter> UpdateAsync(int id, Adopter updatedAdopter)
        {
            updatedAdopter.Id = id;
            return await _repo.UpdateAsync(updatedAdopter);
        }

        public async Task<Adopter> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
