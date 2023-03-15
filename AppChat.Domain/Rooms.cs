using AppChat.Domain.Abstractions;
using AppChat.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace AppChat.Domain
{
    public class Rooms
    {
        private readonly IRoomManager _roomManager;
        private readonly IMediator _mediator;
        private readonly List<Room> _trackedRooms = new List<Room>();

        public Rooms(IRoomManager roomManager, IMediator mediator)
        {
            _roomManager = roomManager;
            _mediator = mediator;
        }

        public async Task CreateAsync(Room room)
        {
            await _roomManager.CreateAsync(room);
        }

        public Task<Room> OpenAsync(string roomName)
        {
            return _roomManager.OpenAsync(roomName);
        }

        public async Task<IEnumerable<Room>> OpenOrStartRooms(string withUser, string connectionId)
        {
            var users = await _roomManager.AllUsersToChatWith(withUser);
            var currentUser = users.FirstOrDefault(user => user.UserName == withUser);
            if (currentUser == null)
                throw new UserNotFound(withUser);
            currentUser.IsOnline(connectionId);
            var rooms = users.Except(new[] { currentUser }).SelectMany(user =>
            {
                if (!user.Rooms.Any(user => user.Users.Any(a => a.UserName == withUser)))
                {
                    var newRoom = new Room
                    {
                        DisplayName = user.Name,
                        Thumbnail = string.Empty,
                        Id = Guid.NewGuid().ToString()
                    };
                    newRoom.Users.Add(currentUser);
                    newRoom.Users.Add(user);
                    user.Rooms.Add(newRoom);
                }
                var room = user.Rooms.First(user => user.Users.Any(a => a.UserName == withUser));
                room.DynamicDisplayName = user.Name;
                return user.Rooms;
            }).ToList();
            _trackedRooms.AddRange(rooms);
            return rooms;
        }

        public async Task CloseRoomsWithAsync(string user)
        {

        }

        public async Task DetectChangesAsync()
        {
            await _roomManager.UpdateStatesAsync();
        }
    }
}