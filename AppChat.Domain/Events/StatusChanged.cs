namespace AppChat.Domain.Events
{
    public class StatusChanged : DomainEvent
    {
        public ConnectionStatus OldStatus { get; set; }
        public Room Room { get; set; }
    }
}
