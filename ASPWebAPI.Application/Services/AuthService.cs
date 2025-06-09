using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using ASPWebAPI.DTOs.User;
using System.Security.Cryptography;

namespace ASPWebAPI.BLL.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(IUserRepository userRepository, JwtService jwtService, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<(string accessToken, string refreshToken)?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            var accessToken = _jwtService.GenerateToken(user.Id, user.Email, user.Role);
            var refreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                UserId = user.Id,
            });

            return (accessToken, refreshToken);
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        public async Task<(string accessToken, string refreshToken)?> RefreshAsync(string oldRefreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(oldRefreshToken);
            if (token == null || token.IsRevoked || token.Expires < DateTime.UtcNow)
                return null;

            await _refreshTokenRepository.RevokeAsync(oldRefreshToken);

            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null) return null;

            var newAccessToken = _jwtService.GenerateToken(user.Id, user.Email, user.Role);
            var newRefreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                UserId = user.Id
            });

            return (newAccessToken, newRefreshToken);
        }

        public async Task<bool> RevokeAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null || token.IsRevoked)
                return false;

            await _refreshTokenRepository.RevokeAsync(refreshToken);
            return true;
        }

        public async Task<bool> RegisterAsync(string email, string password, string role)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
                return false;

            var hashed = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Email = email,
                PasswordHash = hashed,
                Role = role
            };

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
                return false;

            var hashed = BCrypt.Net.BCrypt.HashPassword(password);

            await _userRepository.RegisterAsync(email, hashed);
            return true;
        }
    }
}
