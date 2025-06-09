using ASPWebAPI.Domain.Entities;

namespace ASPWebAPI.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> RegisterAsync(string email, string password);
        //Task<User?> RegisterWithRole(string email, string password);
    }
}
