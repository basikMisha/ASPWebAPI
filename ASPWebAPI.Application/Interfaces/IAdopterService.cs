using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.BLL.Interfaces
{
    public interface IAdopterService
    {
        IEnumerable<Adopter> GetAll();
        Adopter GetById(int id);
        Adopter Add(Adopter adopter);
        Adopter Update(int id, Adopter updatedAdopter);
        Adopter DeleteById(int id);
    }
}
