using Microsoft.Extensions.DependencyInjection;

namespace LootCouncil.Engine.DependencyInjection
{
    public static class EnginesContainerRegistrar
    {
        public static IServiceCollection AddApplicationEngines(this IServiceCollection services)
        {
            return services.AddScoped<IJwtEngine, JwtEngine>()
                .AddScoped<IUserEngine, UserEngine>()
                .AddScoped<IThatsMyBisDataEngine, ThatsMyBisDataEngine>()
                .AddScoped<IPreVoteConfigurationEngine, PreVoteConfigurationEngine>()
                .AddScoped<IPreVoteGenerationEngine, PreVoteGenerationEngine>();
        }
    }
}