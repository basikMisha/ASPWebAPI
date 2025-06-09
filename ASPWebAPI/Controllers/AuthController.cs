using ASPWebAPI.BLL.Services;
using ASPWebAPI.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebAPI.Api.Controllers
{
    /// <summary>
    /// Auth controller for handling registration and login operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
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
            var result = await _authService.RegisterUserAsync(dto.Email, dto.Password);
            if (!result)
                return BadRequest("User already exists");

            return Ok("Registration successful");
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        /// <returns>
        /// Returns a Jwt token if successful, 401 otherwise.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            if (token == null)
                return Unauthorized("Invalid email or password");

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
            var result = await _authService.RegisterAsync(dto.Email, dto.Password, dto.Role);
            if (!result)
                return BadRequest("User already exists");

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
            var result = await _authService.RefreshAsync(dto.RefreshToken);
            if (result == null)
                return Unauthorized();

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
            var result = await _authService.RevokeAsync(dto.RefreshToken);
            if (!result)
                return NotFound("Token not found or already revoked");

            return Ok("Refresh token successfully revoked");
        }
    }
}
