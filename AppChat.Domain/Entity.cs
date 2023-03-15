using AppChat.Domain.Events;

namespace AppChat.Domain
{
    public class ChatBotCommandDescriptor
    {
        public string Command { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }

    public class Entity
    {
        private List<DomainEvent> _events = new List<DomainEvent>();

        public IEnumerable<DomainEvent> Events { get => _events; }

        protected void AddEvent(DomainEvent @event) 
        { 
            _events.Add(@event); 
        }
        
        public IEnumerable<DomainEvent> EventsToDispatch()
        {
            var events = this._events.ToList();
            _events.Clear();
            return events;
        }
    }
}