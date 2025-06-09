using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IAdopterRepository : IRepository<Adopter>
    {
        Task<bool> ExistsAsync(int id);
    }
}
