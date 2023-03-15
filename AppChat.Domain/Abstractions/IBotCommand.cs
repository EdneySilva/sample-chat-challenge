namespace AppChat.Domain.Abstractions
{
    public interface IBotCommand
    {
        public Task Run(ThreadMessage message, IServiceProvider service);
    }
}
