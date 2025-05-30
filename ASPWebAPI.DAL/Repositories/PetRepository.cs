using ASPWebAPI.Domain.Interfaces;
using ASPWebAPI.Domain.Entities;
namespace ASPWebAPI.DAL.Repositories
{
    public class PetRepository : IPetRepository
    {
        private List<Pet> _pets = new()
        {
            new Pet { Id = 1, Species = "dog", Age = 12, Description = "dog", isAdopted = false, VolunteerId =  1}
        };

        public IEnumerable<Pet> GetAll() => _pets;

        public Pet GetById(int id) => _pets.FirstOrDefault(p => p.Id == id);

        public Pet Add(Pet pet)
        {
            pet.Id = _pets.Max(p => p.Id) + 1;
            _pets.Add(pet);
            return pet;
        }

        public Pet Update(int id, Pet updatedPet)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet == null) return null;
            pet.Species = updatedPet.Species;
            pet.Age = updatedPet.Age;
            pet.Description = updatedPet.Description;
            pet.isAdopted = updatedPet.isAdopted;
            pet.PhotoUrl = updatedPet.PhotoUrl;
            pet.VolunteerId = updatedPet.VolunteerId;

            if(updatedPet.Volunteer is not null)
            {
                pet.Volunteer = updatedPet.Volunteer;
            }

            return pet;
        }

        public Pet DeleteById(int id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if(pet is not null)
            {
                _pets.Remove(pet);
            }
            return pet;
        }

    }
}
