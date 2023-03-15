using Microsoft.Extensions.DependencyInjection;

namespace AppChat.Domain.DependencyInjection
{
    public static class DomainDependencies
    {
        public static IServiceCollection AddRooms(this IServiceCollection services, Action<MediatRServiceConfiguration> configuration)
        {
            services.AddScoped<Rooms>();
            services.AddScoped<ChatBot>();
            services.AddMediatR(configuration);
            return services;
        }
    }
}
