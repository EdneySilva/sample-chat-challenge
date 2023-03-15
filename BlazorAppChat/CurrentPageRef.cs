using AppChat.Domain;
using Microsoft.AspNetCore.Components;
using System.Collections;

namespace BlazorAppChat
{
    public class AppRoomManager : IEnumerable<Room>
    {
        private event EventHandler<IEnumerable<Room>> onRoomAdded;
        private event EventHandler<Room> onRoomOpenned;
        private readonly List<Room> roomList = new List<Room>();

        public EventCallback ChangeRoom(Room room)
        {
            onRoomOpenned?.Invoke(this, room);
            return new EventCallback();
        }

        public void OnRoomOpenned(Action<object, Room> room)
        {
            onRoomOpenned += new EventHandler<Room>(room);
        }

        public void OnRoomsAdded(Action<object, IEnumerable<Room>> rooms)
        {
            onRoomAdded += new EventHandler<IEnumerable<Room>>(rooms);
        }

        public void AddRoom(Room room)
        {
            roomList.Add(room);
        }

        public void AddRooms(IEnumerable<Room> rooms)
        {
            roomList.AddRange(rooms);
            onRoomAdded?.Invoke(this, rooms);
        }

        public IEnumerator<Room> GetEnumerator()
        {
            return roomList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return roomList.GetEnumerator();
        }
    }
}