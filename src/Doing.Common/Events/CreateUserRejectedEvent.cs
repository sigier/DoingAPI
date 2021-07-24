namespace Doing.Common.Events
{
    public class CreateUserRejectedEvent : IRejectedEvent
    {
        public string Email { get; }
        public string ReasonError { get; }

        public string CodeError { get; }

        protected CreateUserRejectedEvent(){}
      
        public CreateUserRejectedEvent(string email, string reasonError, string codeError)
        {
            Email = email;
            ReasonError = reasonError;
            CodeError = codeError;
        }

    
    }
}