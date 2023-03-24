using BlazorAppChat.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using AppChat.Infrastructure.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AppChat.Domain.DependencyInjection;

namespace BlazorAppChat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddStorage((options) =>
                {
                    options.UseSqlServer(connectionString);
                })
                .AddManagers()
            .AddRooms((config) =>
            {
                config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
            });
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<AppRoomManager>();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.MapHub<ChannelHub>(ChannelHub.HubUrl);
  

            app.Run();
        }
    }
}