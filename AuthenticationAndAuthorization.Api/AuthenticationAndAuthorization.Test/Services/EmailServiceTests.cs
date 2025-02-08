using AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AuthenticationAndAuthorization.Test.Services
{
    public class EmailServiceTests
    {
        [Fact]
        public async Task SendEmailAsync_WithValidInput_CallsSendGrid()
        {
            // Arrange
            var mockSendGridClient = new Mock<ISendGridClient>();
            var logger = new Mock<ILogger<SendGridEmailService>>();
            var config = new Mock<IConfiguration>();
            var emailService = new SendGridEmailService(logger.Object, config.Object);

            // Act
            await emailService.SendEmailAsync("test@example.com", "Subject", "Message");

            // Assert
            mockSendGridClient.Verify(x => x.SendEmailAsync(It.IsAny<SendGridMessage>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
