namespace AppChat.Domain.Events
{
    public class MessageRead : DomainEvent
    {
        public ThreadMessage ThreadMessage { get; set; }
    }
}
