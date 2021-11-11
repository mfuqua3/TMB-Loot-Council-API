using Discord;
using Discord.Rest;
using LootCouncil.Service.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LootCouncil.Service.DependencyInjection
{
    public static class ServicesContainerRegistrar
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<IAccountService, AccountService>()
                .AddTransient<DiscordRestClient>();
            
        }
    }
}