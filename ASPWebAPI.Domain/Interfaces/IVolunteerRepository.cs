using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IVolunteerRepository : IRepository<Volunteer>
    {
        Task<bool> ExistsAsync(int id);
    }
}
