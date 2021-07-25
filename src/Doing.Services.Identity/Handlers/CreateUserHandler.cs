using System;
using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Events;
using Doing.Common.Exceptions;
using Doing.Services.Identity.Domain.Services;
using Microsoft.Extensions.Logging;
using RawRabbit;

namespace Doing.Services.Identity.Handlers
{
    public class  CreateUserHandler: ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;

        private readonly IUserService _userService;

        private readonly ILogger _logger;

        public CreateUserHandler(IBusClient bus,
            IUserService userService,
            ILogger<CreateUserHandler> logger)
        {
            _bus = bus;
            _logger = logger;
            _userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            
             _logger.LogInformation($"Creating user {command.Email} {command.Name}");

            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Name);

                await _bus.PublishAsync(new CreateUserEvent(command.Email, command.Name));


                return;
            }
            catch (DoingException ex)
            {
                _logger.LogError(ex, ex.Message);
                
                await _bus.PublishAsync(
                 new CreateUserRejectedEvent(command.Email, ex.Code, ex.Message));

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await _bus.PublishAsync(
                 new CreateUserRejectedEvent(command.Email, "error", ex.Message));
            }
        
        }
    }
}