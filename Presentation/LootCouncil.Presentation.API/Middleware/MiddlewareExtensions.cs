using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LootCouncil.Presentation.API.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void AddCustomMiddleware(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        public static void UseExceptionHandling(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}