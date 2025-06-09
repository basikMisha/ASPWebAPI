using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Dapper;
using System.Data;

namespace ASPWebAPI.DAL.Dapper.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDbConnection _db;

        public RefreshTokenRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task AddAsync(RefreshToken token)
        {
            var sql = @"INSERT INTO auth.RefreshToken(Token, Expires, IsRevoked, UserId)
                      VALUES (@Token, @Expires, @IsRevoked, @UserId)";
            await _db.ExecuteAsync(sql, token);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            var sql = @"SELECT * FROM auth.RefreshToken WHERE Token = @Token";
            return await _db.QueryFirstOrDefaultAsync<RefreshToken>(sql, new {Token = token});
        }

        public async Task RevokeAsync(string token)
        {
            var sql = @"UPDATE auth.RefreshToken SET IsRevoked = 1 WHERE Token = @Token";
            await _db.ExecuteAsync(sql, new {Token = token});
        }
    }
}
