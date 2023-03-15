namespace AppChat.Domain.Abstractions
{
    public interface IRoomManager
    {
        Task CreateAsync(Room room);
        public Task<bool> ExistsAsync(string roomName);
        public Task<Room> OpenAsync(string roomName);
        Task<IEnumerable<Room>> Rooms();
        Task<IEnumerable<Account>> AllUsersToChatWith(string with);
        Task UpdateStatesAsync();
    }
}
