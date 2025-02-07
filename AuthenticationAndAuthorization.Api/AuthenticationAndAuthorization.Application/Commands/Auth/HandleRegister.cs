using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using AuthenticationAndAuthorization.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AuthenticationAndAuthorization.Application.Commands.Auth
{
    public class HandleRegister
    {
        public class Command : IRequest<StandardResponse<string>>
        {
            public Command(RegisterModel registerModel)
            {
                RegisterModel = registerModel;
            }

            public RegisterModel RegisterModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, StandardResponse<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly UserManager<ApplicationUser> _userManager;
            public Handler(ILogger<Handler> logger,
                UserManager<ApplicationUser> userManager)
            {
                _logger = logger;
                _userManager = userManager;
            }

            public async Task<StandardResponse<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = request.RegisterModel.UserName,
                        Email = request.RegisterModel.Email,
                        FullName = request.RegisterModel.FullName
                    };
                    var result = await _userManager.CreateAsync(user, request.RegisterModel.Password);
                    if (result.Succeeded)
                        return StandardResponse<string>.SuccessMessage(code: "200", message: "User registered successfully", data: "successful.");
                    return StandardResponse<string>.ErrorMessage(code: result?.Errors?.FirstOrDefault()?.Code, message: result?.Errors?.FirstOrDefault()?.Description);
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
