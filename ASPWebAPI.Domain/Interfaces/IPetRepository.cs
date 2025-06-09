using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IPetRepository : IRepository<Pet> 
    {
        Task<bool> ExistsAsync(int id);
    }
}
