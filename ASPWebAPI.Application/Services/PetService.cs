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

        public IEnumerable<Pet> GetAll() => _repo.GetAll();

        public Pet GetById(int id) => _repo.GetById(id);

        public Pet Add(Pet pet)
        {
            _repo.Add(pet);
            return pet;
        }

        public Pet Update(int id, Pet updatedPet)
        {
            var pet = _repo.GetById(id);
            if (pet == null) return null;
            pet.Id = id;
            return _repo.Update(id, updatedPet);
        } 

        public Pet DeleteById(int id) => _repo.DeleteById(id);
    }
}
