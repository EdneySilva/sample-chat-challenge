using AppChat.Domain.Abstractions;
using AppChat.Domain.Abstractions.Storage;
using AppChat.Infrastructure.Security;
using AppChat.Infrastructure.Storage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AppChat.Infrastructure.DependencyInjection
{
    public static class StorageDependencies
    {
        public static IServiceCollection AddStorage(this IServiceCollection services, Action<DbContextOptionsBuilder> configurationOptions)
        {
            services.AddScoped<IBotStorage, BotStorage>();
            services.AddScoped((service) =>
            {
                var builderItem = new DbContextOptionsBuilder<AppChatDbContext>();
                configurationOptions(builderItem);
                var mediator = service.GetRequiredService<IMediator>();
                return new AppChatDbContext(mediator, builderItem.Options);
            }).AddDbContext<AppChatDbContext>();
            return services;
        }

        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IRoomManager, RoomManager>();
            services.AddScoped<IAccountManager, UserManager>();
           return services;
        }
    }
}
