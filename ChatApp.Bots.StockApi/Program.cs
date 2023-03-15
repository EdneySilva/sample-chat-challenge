
using AppChat.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Bots.StockApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/api/stock", async (HttpContext httpContext, [FromBody] ThreadMessage message) =>
            {
                var replayTo = httpContext.Request.Query.ContainsKey("webhook") ? httpContext.Request.Query["webhook"].ToString() : string.Empty;
                var service = httpContext.RequestServices;
                var httpFactory = service.GetService<IHttpClientFactory>();
                var httpClient = httpFactory.CreateClient();
                var stockCode = message.Content.Split("=").Last();
                //string stockCode = "aapl.us";
                var response = await httpClient.GetAsync($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv");
                var thread = new ThreadMessage();
                thread.From = "BOT";
                thread.To = "*";
                thread.CreatedAt = DateTime.UtcNow;
                thread.ContentType = "text";
                thread.MessageId = Guid.NewGuid().ToString("N");
                thread.Room = message.Room;
                if (!response.IsSuccessStatusCode)
                {
                    thread.Content = $"An error occurred when try to get stock information.\n{response.ReasonPhrase}";
                }
                else
                {
                    var responseText = await response.Content.ReadAsStringAsync();
                    var entries = responseText.Split("\r\n").Skip(1).First().Split(',');
                    var result = $"{entries[0]} quote is {double.Parse(entries[5]):C} per share.";
                    thread.Content = result;
                    thread.ChatRoom = new Room();
                }
                if (!replayTo.Any())
                    return;
                var responseFromWH = await httpClient.PostAsJsonAsync(replayTo, thread, CancellationToken.None);
                var responseStringFromWH = await responseFromWH.Content.ReadAsStringAsync();
            })
            .WithName("GetStock")
            .AllowAnonymous();

            app.Run();
        }
    }
}