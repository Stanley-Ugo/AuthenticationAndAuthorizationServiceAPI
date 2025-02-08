using AuthenticationAndAuthorization.Api.Controllers;
using AuthenticationAndAuthorization.Application.Commands.Auth;
using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthenticationAndAuthorization.Test.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _authController = new AuthController(_mockMediator.Object) { };
        }

        [Fact]
        public async Task Register_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var registerModel = new RegisterModel { Email = "test@example.com", Password = "Password123!", FullName = "Test User" };
            var response = new StandardResponse<string> { Status = true, Code = "200", Message = "User registered successfully" };
            _mockMediator.Setup(x => x.Send(It.IsAny<HandleRegister.Command>(), default))
                .ReturnsAsync(response);

            // Act
            var result = await _authController.Register(registerModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var standardResponse = Assert.IsType<StandardResponse<string>>(okResult.Value);
            Assert.True(standardResponse.Status);
            Assert.Equal("User registered successfully", standardResponse.Message);
        }

        [Fact]
        public async Task Login_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var loginModel = new LoginModel { Email = "test@example.com", Password = "Password123!" };
            var response = new StandardResponse<string> { Status = true, Code = "200", Message = "Login successful" };
            _mockMediator.Setup(x => x.Send(It.IsAny<HandleLogin.Command>(), default))
                .ReturnsAsync(response);

            // Act
            var result = await _authController.Login(loginModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var standardResponse = Assert.IsType<StandardResponse<string>>(okResult.Value);
            Assert.True(standardResponse.Status);
            Assert.Equal("Login successful", standardResponse.Message);
        }
    }
}
