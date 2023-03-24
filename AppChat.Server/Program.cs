using AppChat.Domain;
using AppChat.Domain.Abstractions;
using AppChat.Domain.Abstractions.Storage;
using AppChat.Domain.DependencyInjection;
using AppChat.Infrastructure.DependencyInjection;
using AppChat.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppChat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddStorage((options) =>
            {
                options.UseSqlServer(connectionString);
            }).AddManagers()
           .AddRooms((config) =>
            {
                config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IUserConnectionProvider, InMemoryUserConnectionProvider>();
            builder.Services.AddSingleton<ThreadMessageStream>();
            builder.Services.AddHostedService<BotBackgroundWorker>();
            builder.Services.AddMemoryCache();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddControllers();
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

            app.MapControllers();
            app.MapHub<ChannelHub>(ChannelHub.HubUrl);

            using var scope = app.Services.CreateScope();
            var chatBot = scope.ServiceProvider.GetRequiredService<ChatBot>();

            app.Run();
        }
    }
}