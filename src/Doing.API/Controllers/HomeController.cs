using Microsoft.AspNetCore.Mvc;

namespace Doing.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController: ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Content("Signal from Doing API");
        } 
    }
}