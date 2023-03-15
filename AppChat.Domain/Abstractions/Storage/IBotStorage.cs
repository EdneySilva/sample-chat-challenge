using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Domain.Abstractions.Storage
{
    public interface IBotStorage
    {
        Task CreateCommandAsync(ChatBotCommandDescriptor chatBotCommandDescriptor);

        IEnumerable<ChatBotCommandDescriptor> LoadCommands();
    }
}
