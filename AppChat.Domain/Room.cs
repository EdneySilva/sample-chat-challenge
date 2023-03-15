using AppChat.Domain.Events;

namespace AppChat.Domain
{
    public class Room : Entity
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string DynamicDisplayName { get; set; }
        public string Thumbnail { get; set; }
        public virtual ICollection<Account> Users { get; set; } = new List<Account>();
        public virtual ICollection<ThreadMessage> Threads { get; set; } = new List<ThreadMessage>();

        public Account User(string userName) => Users.FirstOrDefault(user => user.UserName == userName);

        public Room ShallowClone()
        {
            var clone = new Room();
            clone.Id = Id;
            clone.DisplayName = DisplayName;
            clone.DynamicDisplayName = DynamicDisplayName;
            clone.Thumbnail = Thumbnail;
            clone.Threads = Threads.Select(item =>
            {
                return item.ShallowClone();
            }).ToList();

            foreach (var user in Users)
                clone.Users.Add(new Account(
                    user.Name, 
                    user.UserName, 
                    user.Thumbnail, 
                    user.Email, 
                    user.PhoneNumber, 
                    user.CurrentConnectionId, 
                    user.Status, 
                    new List<Room>()    
                ));
            return clone;
        }

        public async Task SendMessageAsync(ThreadMessage message)
        {
            message.MessageId = Guid.NewGuid().ToString();
            this.Threads.Add(message);
            this.AddEvent(new MessageSent
            {
                Room = this,
                ThreadMessage = message
            });
        }
    }
}