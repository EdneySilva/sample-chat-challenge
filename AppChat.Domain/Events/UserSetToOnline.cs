namespace AppChat.Domain.Events
{
    public class UserSetToOnline : DomainEvent
    {
        public Account User { get; set; }
    }
}
