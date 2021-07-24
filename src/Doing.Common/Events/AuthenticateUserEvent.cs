namespace Doing.Common.Events
{
    public class AuthenticateUserEvent: IEvent
    { 
        public string Email { get; }

        protected AuthenticateUserEvent(){}
  

        public AuthenticateUserEvent(string email)
        {
            Email = email;
        }

       

        
    }
}