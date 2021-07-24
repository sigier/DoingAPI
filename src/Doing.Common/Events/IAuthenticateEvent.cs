using System;

namespace Doing.Common.Events
{
    public interface IAuthenticateEvent: IEvent
    {
        Guid UserId { get; }
    }
}