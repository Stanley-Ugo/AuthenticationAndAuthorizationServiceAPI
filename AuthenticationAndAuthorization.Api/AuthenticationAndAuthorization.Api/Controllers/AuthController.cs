using AuthenticationAndAuthorization.Application.Commands.Auth;
using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        [HttpPost("register")]
        [Produces("application/json", Type = typeof(StandardResponse<string>))]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var command = new HandleRegister.Command(model);
            var result = await Mediator.Send(command);
            if (result.Status)
                return Ok(result);
            return StatusCode(int.Parse(result.Code), result);
        }

        [HttpPost("login")]
        [Produces("application/json", Type = typeof(StandardResponse<string>))]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var command = new HandleLogin.Command(model);
            var result = await Mediator.Send(command);
            if (result.Status)
                return Ok(result);
            return StatusCode(int.Parse(result.Code), result);
        }

    }
}
