using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        public ApiBaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
