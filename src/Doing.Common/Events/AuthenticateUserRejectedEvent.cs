namespace Doing.Common.Events
{
    public class AuthenticateUserRejectedEvent : IRejectedEvent
    {
        public string Email { get; }

        public string ReasonError { get; }

        public string CodeError { get; }

        protected AuthenticateUserRejectedEvent()
        {
        }

        public AuthenticateUserRejectedEvent(string email, string reasonError, string codeError)
        {
            Email = email;
            ReasonError = reasonError;
            CodeError = codeError;
        }
    }
}