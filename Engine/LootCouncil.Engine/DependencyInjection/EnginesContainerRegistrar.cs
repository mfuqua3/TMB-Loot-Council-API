using Microsoft.Extensions.DependencyInjection;

namespace LootCouncil.Engine.DependencyInjection
{
    public static class EnginesContainerRegistrar
    {
        public static IServiceCollection AddApplicationEngines(this IServiceCollection services)
        {
            return services.AddTransient<IJwtEngine, JwtEngine>();
        }
    }
}