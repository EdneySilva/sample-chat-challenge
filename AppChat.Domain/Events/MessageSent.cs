namespace AppChat.Domain.Events
{
    public class MessageSent : DomainEvent
    {
        public ThreadMessage ThreadMessage { get; set; }
        public Room Room { get; set; }
    }
}
