using System;
using System.Linq;
using System.Threading.Tasks;
using Doing.API.Repositories;
using Doing.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Doing.API.Controllers
{

    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DoingsController : Controller
    {
        private readonly IBusClient _bus;

        private readonly IDoingRepository _doingRepository;

        public DoingsController(IBusClient bus,
                IDoingRepository doingRepository)
        {
            _bus= bus;
            _doingRepository = doingRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateDoing command)
        {
            command.Id =  Guid.NewGuid();

            command.CreatedAt = DateTime.UtcNow;

            command.UserId = Guid.Parse(User.Identity.Name);

            await _bus.PublishAsync(command);

            return Accepted($"doings/{command.Id} published");
        }

        [HttpGet("")]
        public async Task<ActionResult> Get()
        {
            var doings = await _doingRepository
                    .BrowseAllAsync(Guid.Parse(User.Identity.Name));
            return Json(doings.Select(x =>
             new {x.Id, x.Name, x.Category, x.CreatedAt }));

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var doing = await _doingRepository
                    .GetAsync(id);
            
            if (doing == null)
            {
                return NotFound();
            }

            if (doing.UserId == Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }
          
            return Json(doing);
        }

    }
}