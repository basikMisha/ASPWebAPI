using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class PetRepository : EFRepository<Pet>, IPetRepository
    {
        public PetRepository(PetCenterDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(p => p.Id == id);
        }
    }
}
