using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace ASPWebAPI.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            JwtService jwtService,
            IRefreshTokenRepository refreshTokenRepository,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _refreshTokenRepository = refreshTokenRepository;
            _logger = logger;
        }

        /// <summary>
        /// Performs user authentication and returns a pair of tokens.
        /// </summary>
        public async Task<(string accessToken, string refreshToken)?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("Login failed: user with email {Email} not found", email);
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                _logger.LogWarning("Login failed: invalid password for email {Email}", email);
                return null;
            }

            _logger.LogInformation("User {Email} successfully logged in", email);

            var accessToken = _jwtService.GenerateToken(user.Id, user.Email, user.Role);
            var refreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                UserId = user.Id,
            });

            _logger.LogInformation("Refresh token issued for user {Email}", email);

            return (accessToken, refreshToken);
        }

        /// <summary>
        /// Generates a secure refresh token.
        /// </summary>
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }

        /// <summary>
        /// Updates access and refresh tokens based on an existing refresh token.
        /// </summary>
        public async Task<(string accessToken, string refreshToken)?> RefreshAsync(string oldRefreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(oldRefreshToken);
            if (token == null || token.IsRevoked || token.Expires < DateTime.UtcNow)
            {
                _logger.LogWarning("Token refresh failed: invalid or expired token");
                return null;
            }

            await _refreshTokenRepository.RevokeAsync(oldRefreshToken);
            _logger.LogInformation("Old refresh token revoked");

            var user = await _userRepository.GetByIdAsync(token.UserId);
            if (user == null)
            {
                _logger.LogError("Token refresh failed: user not found (ID: {UserId})", token.UserId);
                return null;
            }

            var newAccessToken = _jwtService.GenerateToken(user.Id, user.Email, user.Role);
            var newRefreshToken = GenerateRefreshToken();

            await _refreshTokenRepository.AddAsync(new RefreshToken
            {
                Token = newRefreshToken,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                UserId = user.Id
            });

            _logger.LogInformation("Tokens refreshed for user {Email}", user.Email);

            return (newAccessToken, newRefreshToken);
        }

        /// <summary>
        /// Revokes (deletes) an existing refresh token.
        /// </summary>
        public async Task<bool> RevokeAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            if (token == null)
            {
                _logger.LogWarning("Token revoke failed: token not found");
                return false;
            }

            if (token.IsRevoked)
            {
                _logger.LogWarning("Token revoke skipped: token already revoked");
                return false;
            }

            await _refreshTokenRepository.RevokeAsync(refreshToken);
            _logger.LogInformation("Refresh token revoked successfully");

            return true;
        }

        /// <summary>
        /// Registers a new user with the specified role.
        /// </summary>
        public async Task<bool> RegisterAsync(string email, string password, string role)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
            {
                _logger.LogWarning("Registration failed: user with email {Email} already exists", email);
                return false;
            }

            var hashed = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User
            {
                Email = email,
                PasswordHash = hashed,
                Role = role
            };

            await _userRepository.AddAsync(user);
            _logger.LogInformation("User {Email} registered with role {Role}", email, role);

            return true;
        }

        /// <summary>
        /// Simplified registration of a regular user (role User).
        /// </summary>
        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            var existing = await _userRepository.GetByEmailAsync(email);
            if (existing != null)
            {
                _logger.LogWarning("User registration failed: user with email {Email} already exists", email);
                return false;
            }

            var hashed = BCrypt.Net.BCrypt.HashPassword(password);

            await _userRepository.RegisterAsync(email, hashed);
            _logger.LogInformation("New user registered: {Email}", email);

            return true;
        }
    }
}
