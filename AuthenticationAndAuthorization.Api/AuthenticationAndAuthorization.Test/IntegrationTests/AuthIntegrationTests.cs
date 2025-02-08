using AuthenticationAndAuthorization.Api.Controllers;
using AuthenticationAndAuthorization.Application.Commands.Auth;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using AuthenticationAndAuthorization.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorization.Test.IntegrationTests
{
    public class AuthIntegrationTests : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        private readonly AuthController _authController;

        public AuthIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAuthDb")
                .Options;
            _dbContext = new AppDbContext(options);

            var serviceProvider = new ServiceCollection()
                .AddMediatR(m => m.RegisterServicesFromAssembly(typeof(HandleRegister).Assembly))
                .AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestAuthDb"))
                .BuildServiceProvider();

            _mediator = serviceProvider.GetRequiredService<IMediator>();
            _authController = new AuthController(_mediator);
        }

        [Fact]
        public async Task Register_ValidModel_CreatesUserInDatabase()
        {
            // Arrange
            var registerModel = new RegisterModel { Email = "test@example.com", Password = "Password123!", FullName = "Test User" };

            // Act
            var result = await _authController.Register(registerModel);

            // Assert
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.NotNull(user);
            Assert.Equal("Test User", user.FullName);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
