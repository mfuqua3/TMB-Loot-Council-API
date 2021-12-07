using Discord.Rest;
using LootCouncil.Service.Core;
using LootCouncil.Service.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LootCouncil.Service.DependencyInjection
{
    public static class ServicesContainerRegistrar
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IImportService, ImportService>()
                .AddScoped<IGuildService, GuildService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IUserDataService, UserDataService>()
                .AddTransient<DiscordRestClient>();
            
        }
    }
}