using System;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Doing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoingsController : ControllerBase
    {
        private readonly IBusClient _bus;

        public DoingsController(IBusClient bus)
        {
            _bus= bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateDoing command)
        {
            command.Id =  Guid.NewGuid();

            command.CreatedAt = DateTime.UtcNow;

            await _bus.PublishAsync(command);

            return Accepted($"doings/{command.Id} published");
        }

    }
}