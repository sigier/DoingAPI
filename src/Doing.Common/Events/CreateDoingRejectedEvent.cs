using System;

namespace Doing.Common.Events
{
    public class CreateDoingRejectedEvent : IRejectedEvent
    {
        

        public Guid Id { get; }

        public string ReasonError { get; }

        public string CodeError { get; }

        protected  CreateDoingRejectedEvent() { }

        public CreateDoingRejectedEvent(Guid id, string reasonError, string codeError)
        {
            Id = id;
            ReasonError = reasonError;
            CodeError = codeError;
        }
            
        
    }
}