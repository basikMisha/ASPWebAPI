using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class PetService : IPetService
    {
        private IPetRepository _repo;

        public PetService(IPetRepository repo)
        {
            _repo = repo;
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
            return await _repo.AddAsync(pet);
        }
        
        public async Task<Pet> UpdateAsync(int id, Pet updatedPet)
        {
            updatedPet.Id = id;
            return await _repo.UpdateAsync(updatedPet);
        }

        public async Task<Pet> DeleteByIdAsync(int id)
        {
            return await _repo.DeleteByIdAsync(id);
        }
    }
}
