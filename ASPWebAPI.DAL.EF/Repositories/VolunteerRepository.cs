using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class VolunteerRepository : EFRepository<Volunteer>, IVolunteerRepository
    {
        public VolunteerRepository(PetCenterDbContext context) : base(context) { }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(a => a.Id == id);
        }
    }
}
