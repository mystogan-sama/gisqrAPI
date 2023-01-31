using gisAPI.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IDbContext? _context;
        private IMediator? _mediator;
        private IConfiguration? _config;
        private ILogger<BaseController>? _logger;

        protected IDbContext? Context => _context ??=
          HttpContext.RequestServices.GetService<IDbContext>();

        protected IMediator? Mediator => _mediator ??=
          HttpContext.RequestServices.GetService<IMediator>();

        protected IConfiguration? Config => _config ??=
          HttpContext.RequestServices.GetService<IConfiguration>();

        protected ILogger<BaseController>? Logger => _logger ??=
          HttpContext.RequestServices.GetService<ILogger<BaseController>>();
    }
}