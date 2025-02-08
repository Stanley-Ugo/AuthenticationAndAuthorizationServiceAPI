
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService.Implementations
{
    public class SendGridEmailService : IEmailService
    {
        private readonly ILogger<SendGridEmailService> _logger;
        private readonly IConfiguration _config;
        public SendGridEmailService(ILogger<SendGridEmailService> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var smtpClient = new SmtpClient(_config["SendGrid:SmtpHost"])
                {
                    Port = int.Parse(_config["SendGrid:SmtpPort"]),
                    Credentials = new NetworkCredential(_config["SendGrid:Username"], _config["SendGrid:Password"]),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_config["SendGrid:FromEmail"]),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
