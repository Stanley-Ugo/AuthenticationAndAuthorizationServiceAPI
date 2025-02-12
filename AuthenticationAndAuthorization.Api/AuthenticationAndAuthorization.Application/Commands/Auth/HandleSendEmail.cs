using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using AuthenticationAndAuthorization.Infrastructure.ExternalServices.EmailService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthenticationAndAuthorization.Application.Commands.Auth
{
    public class HandleSendEmail
    {
        public class Command : IRequest<StandardResponse<string>>
        {
            public Command(SendEmailModel sendEmailModel)
            {
                SendEmailModel = sendEmailModel;
            }

            public SendEmailModel SendEmailModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, StandardResponse<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly IEmailService _emailService;
            public Handler(ILogger<Handler> logger,
                IEmailService emailService)
            {
                _logger = logger;
                _emailService = emailService;   
            }

            public async Task<StandardResponse<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    await _emailService.SendEmailAsync(request.SendEmailModel.Email, request.SendEmailModel.Subject, request.SendEmailModel.Message);
                    return StandardResponse<string>.SuccessMessage(code: "200", message: "Message sent", data: "successful.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    return StandardResponse<string>.SystemError();
                }
            }
        }
    }
}
