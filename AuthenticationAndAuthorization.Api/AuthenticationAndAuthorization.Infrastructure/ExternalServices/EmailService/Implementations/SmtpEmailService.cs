using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService.Implementations
{
    public class SmtpEmailService : IEmailService
    {
        private  ILogger<SmtpEmailService> _logger;
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly string _userToken;
        public SmtpEmailService(ILogger<SmtpEmailService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _smtpClient = new SmtpClient(configuration["Smtp:Host"], int.Parse(configuration["Smtp:Port"]))
            {
                Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                EnableSsl = true
            };
            _fromEmail = configuration["Smtp:FromEmail"];
            _userToken = configuration["Smtp:UserToken"];
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage(from: _fromEmail, to: email, subject: subject, body: message);
                _smtpClient.SendAsync(mailMessage, _userToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
