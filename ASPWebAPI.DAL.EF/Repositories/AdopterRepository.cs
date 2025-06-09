using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class AdopterRepository : EFRepository<Adopter>, IAdopterRepository
    {
        public AdopterRepository(PetCenterDbContext context) : base(context) { }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(a => a.Id == id);
        }
    }
}
