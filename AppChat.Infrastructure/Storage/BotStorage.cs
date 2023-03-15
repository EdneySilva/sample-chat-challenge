using AppChat.Domain;
using AppChat.Domain.Abstractions.Storage;

namespace AppChat.Infrastructure.Storage
{
    internal class BotStorage : IBotStorage
    {
        private readonly AppChatDbContext _appChatDbContext;

        public BotStorage(AppChatDbContext appChatDbContext)
        {
            _appChatDbContext = appChatDbContext;
        }

        public async Task CreateCommandAsync(ChatBotCommandDescriptor chatBotCommandDescriptor)
        {
            await _appChatDbContext.AddAsync(chatBotCommandDescriptor);
            await _appChatDbContext.SaveChangesAsync();
        }

        public IEnumerable<ChatBotCommandDescriptor> LoadCommands()
        {
            return _appChatDbContext.Set<ChatBotCommandDescriptor>().AsQueryable();
        }
    }
}
