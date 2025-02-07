using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
