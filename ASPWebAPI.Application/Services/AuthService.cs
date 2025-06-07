using ASPWebAPI.Domain.Entities;
using ASPWebAPI.Domain.Interfaces;

namespace ASPWebAPI.BLL.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            return _jwtService.GenerateToken(user.Email, user.Role);
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
