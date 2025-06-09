using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASPWebAPI.DAL.EF.Repositories
{
    public class RefreshTokenRepository : EFRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(PetCenterDbContext context) : base(context) { }

        public async Task AddAsync(RefreshToken token)
        {
            await base.AddAsync(token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task RevokeAsync(string token)
        {
            var refreshToken = await GetByTokenAsync(token);
            if (refreshToken != null && !refreshToken.IsRevoked)
            {
                refreshToken.IsRevoked = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
