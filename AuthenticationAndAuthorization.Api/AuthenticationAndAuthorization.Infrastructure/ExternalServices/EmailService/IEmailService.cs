namespace AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
