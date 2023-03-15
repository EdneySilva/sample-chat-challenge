
using AppChat.Domain.Abstractions;
using AppChat.Domain.Abstractions.Storage;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AppChat.Domain
{
    public class ChatBot
    {
        private readonly IBotStorage _botStorage;

        private Dictionary<Regex, IBotCommand> Commands { get; } = new Dictionary<Regex, IBotCommand>();

        public ChatBot(IBotStorage botStorage)
        {
            _botStorage = botStorage;
            Load();
        }

        private void Load()
        {
            var commands = _botStorage.LoadCommands();
            foreach (var botCommand in commands)
            {
                var command = (IBotCommand)JsonSerializer.Deserialize(botCommand.Data, Type.GetType(botCommand.Type));
                Commands.Add(new Regex(botCommand.Command), command);
            }
        }

        public async Task AddCommandAsync(Regex regex, IBotCommand command)
        {
            Commands.Add(regex, command);
            await _botStorage.CreateCommandAsync(new ChatBotCommandDescriptor
            {
                Command = regex.ToString(),
                Type = command.GetType().AssemblyQualifiedName,
                Data = JsonSerializer.Serialize(command as object)
            });
        }

        public Task ProcessMessage(ThreadMessage message, IServiceProvider services)
        {
            List<Task> tasks = new List<Task>();
            foreach (var item in Commands)
            {
                if(item.Key.IsMatch(message.Content))
                {
                    tasks.Add(item.Value.Run(message, services));
                }
            }
            return Task.WhenAll(tasks);
            //var collections = Regex.Match(message.Content, "^\\/([^\\s@]+)?=(\\S+)?\\s?(.*)$");
        }
    }
}