using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using AuthenticationAndAuthorization.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuthenticationAndAuthorization.Application.Commands.Auth
{
    public class HandleLogin
    {
        public class Command : IRequest<StandardResponse<string>>
        {
            public Command(LoginModel loginModel)
            {
                LoginModel = loginModel;
            }

            public LoginModel LoginModel { get; set; }
        }

        public class Handler : IRequestHandler<Command, StandardResponse<string>>
        {
            private readonly ILogger<Handler> _logger;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly IConfiguration _configuration;
            public Handler(ILogger<Handler> logger,
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                UserManager<ApplicationUser> userManager)
            {
                _logger = logger;
                _signInManager = signInManager;
                _configuration = configuration;
                _userManager = userManager;
            }

            public async Task<StandardResponse<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var result = await _signInManager.PasswordSignInAsync(request.LoginModel.Email, request.LoginModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByEmailAsync(request.LoginModel.Email);
                        var token = CoreUtilities.GenerateJwtToken(user, _configuration);
                        return StandardResponse<string>.SuccessMessage(code: "200", message: "successful", data: token);
                    }
                    return StandardResponse<string>.ErrorMessage(code: "401", message: "Invalid credentials");
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
