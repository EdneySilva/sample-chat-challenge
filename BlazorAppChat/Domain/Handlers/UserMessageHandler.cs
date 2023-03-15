using AppChat.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BlazorAppChat.Domain.Handlers
{
    public class UserMessageHandler : INotificationHandler<MessageSent>,
         INotificationHandler<MessageDelivered>,
         INotificationHandler<MessageRead>
    {
        private readonly IHubContext<ChannelHub> hubContext;

        public UserMessageHandler(IHubContext<ChannelHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Handle(MessageSent notification, CancellationToken cancellationToken)
        {
            //var users = notification.Room.Users.Where(w => w.Status == AppChat.Domain.ConnectionStatus.Online).ToList();
            //List<string> connections = new List<string>(users.Count);
            //foreach (var item in users)
            //{
            //    if (string.IsNullOrEmpty(item.CurrentConnectionId))
            //        continue;
            //    connections.Add(item.CurrentConnectionId);
            //}
            //await hubContext.Clients.Clients(connections).SendAsync("OnMessageSent", notification.ThreadMessage);
        }

        public async Task Handle(MessageDelivered notification, CancellationToken cancellationToken)
        {
            
        }

        public async Task Handle(MessageRead notification, CancellationToken cancellationToken)
        {
            
        }
    }
}