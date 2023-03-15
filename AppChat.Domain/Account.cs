using AppChat.Domain.Events;

namespace AppChat.Domain
{
    public class Account : Entity
    {
        public Account()
        {

        }

        internal Account(string name, string userName, string thumbnail, string email, string phoneNumber, string currentConnectionId, ConnectionStatus status, ICollection<Room> rooms)
        {
            Name = name;
            UserName = userName;
            Thumbnail = thumbnail;
            Email = email;
            PhoneNumber = phoneNumber;
            CurrentConnectionId = currentConnectionId;
            Status = status;
            Rooms = rooms;
        }

        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Thumbnail { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? CurrentConnectionId { get; protected set; }
        public ConnectionStatus Status { get; set; }
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();

        public void IsOnline(string connectionId)
        {
            Status = ConnectionStatus.Online;
            CurrentConnectionId = connectionId;
            AddEvent(new UserSetToOnline
            {
                User = this
            });
        }

        public void IsOffline()
        {
            Status = ConnectionStatus.Offline;
            CurrentConnectionId = string.Empty;
            AddEvent(new UserSetToOffline
            {
                User = this
            });
        }

        public Account ShallowClone()
        {
            var user = this;
            return new Domain.Account(
                user.Name, user.UserName, user.Thumbnail, user.Email, user.PhoneNumber, user.CurrentConnectionId, user.Status, new List<Room>()
            );
        }
    }
}