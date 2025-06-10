using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ASPWebAPI.Api.Controllers
{
    /// <summary>
    /// Auth controller for handling registration and login operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Register user with default role User
        /// </summary>
        /// <returns>
        /// Returns 400 if the user already exists.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSimpleRegisterDto dto)
        {
            _logger.LogInformation("Attempting to register user: {Email}", dto.Email);

            var result = await _authService.RegisterUserAsync(dto.Email, dto.Password);
            if (!result)
            {
                _logger.LogWarning("Registration failed: user {Email} already exists", dto.Email);
                return BadRequest("User already exists");
            }

            _logger.LogInformation("User {Email} registered successfully", dto.Email);
            return Ok("Registration successful");
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        /// <returns>
        /// Returns a Jwt token and refresh token if successful, 401 otherwise.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            _logger.LogInformation("Login attempt for user: {Email}", dto.Email);

            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            if (token == null)
            {
                _logger.LogWarning("Login failed for user: {Email}", dto.Email);
                return Unauthorized("Invalid email or password");
            }

            _logger.LogInformation("Login successful for user: {Email}", dto.Email);

            var (accessToken, refreshToken) = token.Value;
            return Ok(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Allows an admin to register a new user with a specified role.
        /// Requires the caller to be authorized with the "Admin" role.
        /// </summary>
        /// <returns>
        /// Returns 400 if the user already exists
        /// </returns>
        [HttpPost("admin/register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterWithRole([FromBody] UserRegisterDto dto)
        {
            _logger.LogInformation("Admin attempting to register user: {Email} with role: {Role}", dto.Email, dto.Role);

            var result = await _authService.RegisterAsync(dto.Email, dto.Password, dto.Role);
            if (!result)
            {
                _logger.LogWarning("Admin registration failed: user {Email} already exists", dto.Email);
                return BadRequest("User already exists");
            }

            _logger.LogInformation("Admin registered user {Email} with role {Role}", dto.Email, dto.Role);
            return Ok($"User with role '{dto.Role}' created successfully");
        }

        /// <summary>
        /// Generates a new JWT access token using a valid refresh token.
        /// </summary>
        /// <param name="dto">Request containing the refresh token.</param>
        /// <returns>
        /// Returns 200 OK with new access and refresh tokens;  
        /// Returns 401 Unauthorized if the refresh token is invalid or expired.
        /// </returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto)
        {
            _logger.LogInformation("Token refresh attempt");

            var result = await _authService.RefreshAsync(dto.RefreshToken);
            if (result == null)
            {
                _logger.LogWarning("Token refresh failed: invalid or expired token");
                return Unauthorized();
            }

            _logger.LogInformation("Token refreshed successfully");

            var (accessToken, refreshToken) = result.Value;
            return Ok(new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }

        /// <summary>
        /// Revokes an existing refresh token, preventing its further use.
        /// </summary>
        /// <param name="dto">Request containing the refresh token to revoke.</param>
        /// <returns>
        /// Returns 200 OK if the token was successfully revoked;  
        /// Returns 404 Not Found if the token does not exist or is already revoked.
        /// </returns>
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke([FromBody] RefreshTokenRequestDto dto)
        {
            _logger.LogInformation("Revoking refresh token");

            var result = await _authService.RevokeAsync(dto.RefreshToken);
            if (!result)
            {
                _logger.LogWarning("Revoke failed: token not found or already revoked");
                return NotFound("Token not found or already revoked");
            }

            _logger.LogInformation("Refresh token revoked successfully");
            return Ok("Refresh token successfully revoked");
        }
    }
}
