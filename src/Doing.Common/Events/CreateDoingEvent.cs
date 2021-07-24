using System;

namespace Doing.Common.Events
{
    public class CreateDoingEvent : IAuthenticateEvent
    {
        public Guid UserId { get; }

        public Guid Id { get; }


        public string Category { get; }

        public string Name { get; }

        public string Description { get; }

        public DateTime CreatedAt { get; }

        protected CreateDoingEvent(){}

        public CreateDoingEvent(Guid userId, Guid id, string category, string name)
        {
            UserId = userId;
            Id = id;
            Category = category;
            Name = name;
        }
    }
}