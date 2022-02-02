using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Appeals.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        
        protected IMediator GetMediator() 
        {
            var mediatorService = HttpContext.RequestServices.GetRequiredService<IMediator>();
            _mediator = mediatorService;
            return _mediator;
        }

        internal Guid GetUserId()
        {
            if (User != null && User.Identity != null && !User.Identity.IsAuthenticated)
            {
                var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (value != null)
                    return Guid.Parse(value);
            }
            return Guid.Empty;
        }
    }
}
