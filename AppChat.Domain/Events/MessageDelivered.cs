namespace AppChat.Domain.Events
{
    public class MessageDelivered : DomainEvent
    {
        public ThreadMessage ThreadMessage { get; set; }
    }
}
