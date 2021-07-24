using System.Threading.Tasks;
using Doing.Common.Events;

namespace Doing.API.Handlers
{
    public class CreateDoingHandler : IEventHandler<CreateDoingEvent>
    {
        public async Task HandleAsync(CreateDoingEvent @event)
        {
            await Task.CompletedTask;
            System.Console.WriteLine($"{@event.Name} created");
        }
    }
}