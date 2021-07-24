namespace Doing.Common.Events
{
    public class CreateUserEvent: IEvent
    {
        protected CreateUserEvent() { } 
        
        public CreateUserEvent(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public string Email { get; }

        public string Name { get; }
    }
}