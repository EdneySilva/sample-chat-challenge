using AppChat.Domain.Events;
using AppChat.Server.Controllers;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace BlazorAppChat.Domain.Handlers
{
    public class UserStateHandler : INotificationHandler<UserSetToOnline>, INotificationHandler<UserSetToOffline>
    {
        private readonly IHubContext<ChannelHub> hubContext;

        public UserStateHandler(IHubContext<ChannelHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Handle(UserSetToOnline notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients.All
                .SendAsync(
                    "UserOnline",
                    notification.User.ShallowClone(),
                    cancellationToken
                );
        }

        public async Task Handle(UserSetToOffline notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients.All
                .SendAsync(
                    "UserOffline",
                    notification.User.ShallowClone(),
                    cancellationToken
                );
        }
    }
}