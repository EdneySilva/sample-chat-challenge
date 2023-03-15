namespace AppChat.Domain.Events
{
    public class UserSetToOffline : DomainEvent
    {
        public Account User { get; set; }
    }
}
