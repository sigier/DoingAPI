namespace Doing.Common.Events
{
    public interface IRejectedEvent: IEvent
    {
        string ReasonError { get; }

        string CodeError { get; }
    }
}