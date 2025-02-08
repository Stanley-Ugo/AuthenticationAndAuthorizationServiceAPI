using AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService;
using AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAndAuthorization.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration _configuration)
        {
            var activeEmailServiceProvider = _configuration.GetValue<string>("ActiveEmailServiceProvider");
            switch (activeEmailServiceProvider)
            {
                case "Smtp":
                    services.AddScoped<IEmailService, SmtpEmailService>();
                    break;
                case "SendGrid":
                    services.AddScoped<IEmailService, SendGridEmailService>();
                    break;
                default:
                    break;
            }

            return services;
        }
    }
}
