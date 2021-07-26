using System;
using System.Threading.Tasks;
using Doing.API.DTO;
using Doing.API.Repositories;
using Doing.Common.Events;

namespace Doing.API.Handlers
{
    public class CreateDoingHandler : IEventHandler<CreateDoingEvent>
    {
        private readonly IDoingRepository _doingRepository;

        public CreateDoingHandler(IDoingRepository doingRepository)
        {
            _doingRepository = doingRepository;
        }

        public async Task HandleAsync(CreateDoingEvent @event)
        {
            await _doingRepository.AddAsync(new DoingDto {

                Id = @event.Id,
                UserId = @event.UserId,
                Name = @event.Name,
                Category = @event.Category,
                Description = @event.Description,
                CreatedAt = DateTime.UtcNow
            });
           
            System.Console.WriteLine($"{@event.Name} created");
        }
    }
}