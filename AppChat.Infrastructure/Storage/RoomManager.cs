using AppChat.Domain;
using AppChat.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AppChat.Infrastructure.Storage
{
    internal class RoomManager : IRoomManager
    {
        private readonly AppChatDbContext _appChatDbContext;
        private readonly IMemoryCache _cache;

        public RoomManager(AppChatDbContext appChatDbContext, IMemoryCache cache)
        {
            _appChatDbContext = appChatDbContext;
            _cache = cache;
        }

        public async Task CreateAsync(Room room)
        {
            await _appChatDbContext.AddAsync(room);
            await _appChatDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string roomName)
        {
            return await _appChatDbContext.Set<Room>().AnyAsync(a => a.Id == roomName);
        }

        public Task<Room?> OpenAsync(string roomName)
        {
            return _cache.GetOrCreateAsync(roomName, (entry) =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(5);
                return _appChatDbContext.Set<Room>().AsQueryable().Include(u => u.Users).FirstOrDefaultAsync(room => room.Id == roomName);
            }).ContinueWith(task =>
            {
                _appChatDbContext.Attach(task.Result);
                return task.Result;
            });
        }

        public async Task<IEnumerable<Room>> Rooms()
        {
            return await _appChatDbContext.Set<Room>().AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<Account>> AllUsersToChatWith(string userName)
        {
            return await _appChatDbContext
                .Set<Account>()
                .AsQueryable()
                .Include(p => p.Rooms.Where(room => room.Users.Any(user => user.UserName == userName)))
                .ThenInclude(p => p.Threads.OrderByDescending(o => o.CreatedAt).Take(5).OrderBy(o => o.CreatedAt))
                .ToListAsync();
        }

        public Task UpdateStatesAsync()
        {
            return _appChatDbContext.SaveChangesAsync();
        }
    }
}
