using ASPWebAPI.Api.Controllers;
using ASPWebAPI.BLL.Interfaces;
using ASPWebAPI.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ASPWebAPI.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_ReturnsOk_WhenCredentialsAreValid()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            var mockLogger = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(mockAuthService.Object, mockLogger.Object);
            

            var testEmail = "test@example.com";
            var testPassword = "password";
            var expectedAccessToken = "access123";
            var expectedRefreshToken = "refresh456";

            var dto = new UserLoginDto
            {
                Email = testEmail,
                Password = testPassword
            };

            mockAuthService
                .Setup(s => s.LoginAsync(testEmail, testPassword))
                .ReturnsAsync((expectedAccessToken, expectedRefreshToken));

            // Act
            var result = await controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenResponse = Assert.IsType<TokenResponseDto>(okResult.Value);

            Assert.Equal(expectedAccessToken, tokenResponse.AccessToken);
            Assert.Equal(expectedRefreshToken, tokenResponse.RefreshToken);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            var mockLogger = new Mock<ILogger<AuthController>>();
            var controller = new AuthController(mockAuthService.Object, mockLogger.Object);

            var dto = new UserLoginDto
            {
                Email = "wrong@example.com",
                Password = "wrongpassword"
            };

            mockAuthService
                .Setup(s => s.LoginAsync(dto.Email, dto.Password))
                .ReturnsAsync((ValueTuple<string, string>?)null);

            // Act
            var result = await controller.Login(dto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal("Invalid email or password", unauthorizedResult.Value);
        }
    }
}
