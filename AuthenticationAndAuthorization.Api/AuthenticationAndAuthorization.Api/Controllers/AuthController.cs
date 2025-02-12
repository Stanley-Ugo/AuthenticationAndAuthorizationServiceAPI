using AuthenticationAndAuthorization.Api.Filters;
using AuthenticationAndAuthorization.Application.Commands.Auth;
using AuthenticationAndAuthorization.Application.Utilities;
using AuthenticationAndAuthorization.Application.Variables.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        
        public AuthController(IMediator mediator): base(mediator)
        {
            
        }
        /// <summary>
        /// Register users Endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Login users Endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Admin users only Endpoint
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new { Message = "This is an admin-only endpoint" });
        }

        /// <summary>
        /// regular users only Endpoint
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "User")]
        [HttpGet("user-only")]
        public IActionResult UserOnly()
        {
            return Ok(new { Message = "This is a user-only endpoint" });
        }


        /// <summary>
        /// Protected Endpoint accessible only by ApiKey
        /// </summary>
        /// <returns></returns>
        [ApiKeyAuth]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new { Message = "This is a protected endpoint" });
        }

        /// <summary>
        /// Send Email Endpoint
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("send-email")]
        [Produces("application/json", Type = typeof(StandardResponse<string>))]
        public async Task<IActionResult> SendEmail(SendEmailModel model)
        {
            var command = new HandleSendEmail.Command(model);
            var result = await Mediator.Send(command);
            if (result.Status)
                return Ok(result);
            return StatusCode(int.Parse(result.Code), result);
        }

    }
}
