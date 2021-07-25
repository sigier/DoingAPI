using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Events;
using Doing.Common.Exceptions;
using Doing.Services.Doings.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Doing.Services.Doings.Handlers
{
    public class CreateDoingHandler : ICommandHandler<CreateDoing>
    {
        private readonly IBusClient _bus;

        private readonly IDoingService _doingService;

        private readonly ILogger _logger;


        public CreateDoingHandler(IBusClient bus,
            IDoingService doingService,
            ILogger<CreateDoingHandler> logger)
        {
             _bus = bus;
             _doingService =  doingService;
             _logger = logger;
        }

        public async Task HandleAsync(CreateDoing command)
        {
            _logger.LogInformation($"Creating doing {command.Name}");

            try
            {
               await  _doingService.AddAsync(command.Id, command.UserId,
                command.Category, command.Name, command.Description,
                command.CreatedAt);

               await _bus.PublishAsync(new CreateDoingEvent(
                command.Id, 
                command.UserId,
                command.Category, 
                command.Name
                ));

                return;
            }
            catch (DoingException ex)
            {
                _logger.LogError(ex, ex.Message);
                
                await _bus.PublishAsync(
                 new CreateDoingRejectedEvent(command.Id,ex.Code, ex.Message));

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await _bus.PublishAsync(
                 new CreateDoingRejectedEvent(command.Id, "error", ex.Message));
            }

           
        }
    }
}