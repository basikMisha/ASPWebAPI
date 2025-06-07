using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        public UserRepository(PetCenterDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> RegisterAsync(string email, string password)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = password,
                Role = "User"
            };

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
