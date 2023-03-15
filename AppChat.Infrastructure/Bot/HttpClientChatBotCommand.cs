using AppChat.Domain;
using AppChat.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppChat.Infrastructure.Bot
{
    public class HttpClientChatBotCommand : IBotCommand
    {
        public Uri Uri { get; set; }
        public Uri ReplayTo { get; set; }
        public string Name { get; set; }
        public string Command { get; set; }

        public Task Run(ThreadMessage message, IServiceProvider service)
        {
            var factory = service.GetRequiredService<IHttpClientFactory>();
            var client = factory.CreateClient();
            return client.PostAsync($"{Uri}?webhook={ReplayTo}", new StringContent(JsonSerializer.Serialize(message, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            }), new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")));
            
        }
    }
}
