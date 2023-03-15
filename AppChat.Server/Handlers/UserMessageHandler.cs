using AppChat.Domain.Abstractions;
using AppChat.Domain.Events;
using AppChat.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BlazorAppChat.Domain.Handlers
{
    public class UserMessageHandler : INotificationHandler<MessageSent>,
         INotificationHandler<MessageDelivered>,
         INotificationHandler<MessageRead>
    {
        private readonly IHubContext<ChannelHub> _hubContext;
        private readonly IUserConnectionProvider _userConnectionProvider;

        public UserMessageHandler(IHubContext<ChannelHub> hubContext, IUserConnectionProvider userConnectionProvider)
        {
            _hubContext = hubContext;
            _userConnectionProvider = userConnectionProvider;
        }

        public async Task Handle(MessageSent notification, CancellationToken cancellationToken)
        {
            var users = notification.Room.Users.Where(w => w.Status == AppChat.Domain.ConnectionStatus.Online).ToList();
            List<string> connections = new List<string>(users.Count);
            foreach (var item in users)
            {
                var currentConnectionId = _userConnectionProvider[item.UserName];
                if (string.IsNullOrEmpty(currentConnectionId))
                    continue;
                connections.Add(currentConnectionId);
            }
            await _hubContext.Clients.Clients(connections).SendAsync("OnMessageSent", notification.ThreadMessage.ShallowClone());
        }

        public async Task Handle(MessageDelivered notification, CancellationToken cancellationToken)
        {
            
        }

        public async Task Handle(MessageRead notification, CancellationToken cancellationToken)
        {
            
        }
    }
}