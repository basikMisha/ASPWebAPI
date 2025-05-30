using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.BLL.Interfaces
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAll();
        Pet GetById(int id);
        Pet Add(Pet pet);
        Pet Update(int id, Pet updatedPet);
        Pet DeleteById(int id);
    }
}
