using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Services.Identity.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Doing.Services.Identity.Controllers
{
    
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<JsonResult> LogIn([FromBody] AuthenticateUser command)
        {
            return Json(
                await _userService.LogInAsync(command.Email, command.Password)
            );
        }
    }
}