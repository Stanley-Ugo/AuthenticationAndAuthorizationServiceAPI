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
                return Ok(new { Message = "User registered successfully" });
            return StatusCode(int.Parse(result.Code), result);
        }

    }
}
