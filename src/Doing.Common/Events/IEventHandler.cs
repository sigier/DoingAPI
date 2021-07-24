using System.Threading.Tasks;

namespace Doing.Common.Events
{
   public interface IEventHandler<in T>
        where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}