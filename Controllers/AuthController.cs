using AutoWrapper.Filters;
using gisAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gisAPI.Controllers
{
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [RequestDataLogIgnore]
        [HttpPost("admin/register")]
        public async Task<IActionResult> Register(RegisterUser.Command command) => Ok(await Mediator.Send(command));

        [AllowAnonymous]
        [RequestDataLogIgnore]
        [HttpPost("admin/login")]
        public async Task<IActionResult> Login(LoginUser.Command command) => Ok(await Mediator.Send(command));
    }
}