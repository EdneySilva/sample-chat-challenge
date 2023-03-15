using AppChat.Domain;
using AppChat.Domain.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace AppChat.Server.Controllers
{
    public class ChannelHub : Hub
    {
        public const string HubUrl = "/chat";
        private readonly Rooms _rooms;
        private readonly ChatBot _bot;
        private readonly IServiceProvider _services;
        private readonly IUserConnectionProvider _userConnectionProvider;

        public ChannelHub(Rooms rooms, ChatBot chatBot, IServiceProvider services, IUserConnectionProvider userConnectionProvider)
        {
            _rooms = rooms;
            _bot = chatBot;
            _services = services;
            _userConnectionProvider = userConnectionProvider;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");

            return base.OnConnectedAsync();
        }

        public async Task LoadAsync(string userName)
        {
            _userConnectionProvider.UpdateConnection(userName, Context.ConnectionId);
            var usersToChat = await _rooms.OpenOrStartRooms(userName, Context.ConnectionId);
            await Clients.Caller.SendAsync("OnRoomsLoaded", usersToChat.Select(room => room.ShallowClone()));
            await _rooms.DetectChangesAsync();
        }

        public async Task Broadcast(string username, string message)
        {
            await Clients.All.SendAsync("Broadcast", username, message);
        }

        public async Task SendAsync(ThreadMessage message)
        {
            var room = await _rooms.OpenAsync(message.Room);
            await room.SendMessageAsync(message);
            await _rooms.DetectChangesAsync();
            await _bot.ProcessMessage(message, _services);
        }

        public override async Task OnDisconnectedAsync(Exception? e)
        {
            if (e != null)
                Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            var user = _userConnectionProvider.RemoveConnection(Context.ConnectionId);
            if (user != null)
            {
                await _rooms.CloseRoomsWithAsync(user);
            }
            await base.OnDisconnectedAsync(e);
        }
    }
}