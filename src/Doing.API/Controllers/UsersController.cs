
using System;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Doing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController: ControllerBase
    {
        private readonly IBusClient _bus;

        public UsersController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            await _bus.PublishAsync(command);

            return Accepted();
        }
    }
}