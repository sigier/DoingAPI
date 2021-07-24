using System.Threading.Tasks;
using Doing.Common.Commands;
using Doing.Common.Events;
using RawRabbit;

namespace Doing.Services.Doings.Handlers
{
    public class CreateDoingHandler : ICommandHandler<CreateDoing>
    {
        private readonly IBusClient _bus;
        public CreateDoingHandler(IBusClient bus)
        {
             _bus = bus;
        }

        public async Task HandleAsync(CreateDoing command)
        {
            System.Console.WriteLine($"Creating doing {command.Name}");

            await _bus.PublishAsync(new CreateDoingEvent(
                command.Id, 
                command.UserId,
                command.Category, 
                command.Name
            ));
        }
    }
}