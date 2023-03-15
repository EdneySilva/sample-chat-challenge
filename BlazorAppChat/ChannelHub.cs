using AppChat.Domain;
using Microsoft.AspNetCore.SignalR;

namespace BlazorAppChat
{
    public class ChannelHub : Hub
    {
        public const string HubUrl = "/chat";
        private readonly Rooms _rooms;
        private readonly ChatBot _bot;

        public ChannelHub(Rooms rooms, ChatBot chatBot)
        {
            _rooms = rooms;
            _bot = chatBot;
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");

            return base.OnConnectedAsync();
        }

        public async Task LoadAsync(string userName)
        {
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
            List<string> connections = new List<string>(room.Users.Count);
            await room.SendMessageAsync(message);
            connections.Add(Context.ConnectionId);
            foreach (var item in room.Users.Where(w => !string.IsNullOrWhiteSpace(w?.CurrentConnectionId) && w.CurrentConnectionId != Context.ConnectionId))
            {
                connections.Add(item.CurrentConnectionId);
            }
            await Clients.Clients(connections.AsReadOnly()).SendAsync("OnMessageSent", message);
            await _rooms.DetectChangesAsync();
            await _bot.ProcessMessage(message, null);
        }

        public override async Task OnDisconnectedAsync(Exception? e)
        {
            if (e != null)
                Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            //var rooms = await _rooms.DiscoverAsync();
            //var room = rooms.FirstOrDefault(w => w.CurrentConnectionId == Context.ConnectionId);
            //if(room != null)
            //    room.IsOffline();
            await base.OnDisconnectedAsync(e);
            //await Task.WhenAll(_rooms.UpdateAsync(), base.OnDisconnectedAsync(e));
        }
    }
}