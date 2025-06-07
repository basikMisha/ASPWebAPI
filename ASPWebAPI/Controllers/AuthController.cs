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
        /// Returns 400 if the user already exists.
        /// </summary>
        /// <returns></returns>
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
        /// Returns a Jwt token if successful, 401 otherwise.
        /// </summary>
        /// <returns>adopters</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _authService.LoginAsync(dto.Email, dto.Password);
            if (token == null)
                return Unauthorized("Invalid email or password");

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Allows an admin to register a new user with a specified role.
        /// Requires the caller to be authorized with the "Admin" role.
        /// Returns 400 if the user already exists
        /// </summary>
        /// <returns></returns>
        [HttpPost("admin/register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterWithRole([FromBody] UserRegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto.Email, dto.Password, dto.Role);
            if (!result)
                return BadRequest("User already exists");

            return Ok($"User with role '{dto.Role}' created successfully");
        }
    }
}
