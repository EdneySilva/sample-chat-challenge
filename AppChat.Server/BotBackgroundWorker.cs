using AppChat.Domain;
using AppChat.Domain.Abstractions;
using AppChat.Server.Controllers;
using Microsoft.AspNetCore.SignalR;

namespace AppChat.Server
{
    public class BotBackgroundWorker : IHostedService
    {
        private readonly ThreadMessageStream _threadMessageStream;
        private readonly IServiceScopeFactory _serviceScope;
        List<Task> tasks = new List<Task>();

        public BotBackgroundWorker(ThreadMessageStream threadMessageStream, IServiceScopeFactory serviceScope)
        {
            _threadMessageStream = threadMessageStream;
            _serviceScope = serviceScope;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            tasks.AddRange(
                Enumerable.Range(0, Environment.ProcessorCount).Select(index =>
                {
                    return Task.Run(() =>
                    {
                        do
                        {
                            var threadMessage = _threadMessageStream.ReadNext(cancellationToken);
                            Process(threadMessage);
                        } while (cancellationToken.IsCancellationRequested);
                    });
                })
            );
            return Task.CompletedTask;
        }

        private async Task Process(ThreadMessage threadMessage)
        {
            using(var scope = _serviceScope.CreateScope())
            {
                var rooms = scope.ServiceProvider.GetRequiredService<Rooms>();
                var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ChannelHub>>();
                var connectionProvider = scope.ServiceProvider.GetRequiredService<IUserConnectionProvider>();
                try
                {

                    var room = await rooms.OpenAsync(threadMessage.Room);
                    if (room == null)
                    {
                        // TODO: LOG
                        return;
                    }
                    var connections = room.Users.Select(w => connectionProvider[w.UserName]).Where(s => s != null).ToList();
                    if (!connections.Any())
                    {
                        // TODO: log
                        return;
                    }
                    await hubContext.Clients.Clients(connections).SendAsync("OnMessageSent", threadMessage);
                }
                catch (Exception ex)
                {
                    // TODO: silent log, not throws to not stop bot
                }

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            tasks.Clear();
            return Task.CompletedTask;
        }
    }
}
