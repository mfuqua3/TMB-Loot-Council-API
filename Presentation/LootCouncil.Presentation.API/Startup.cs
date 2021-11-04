using System.Text;
using AspNet.Security.OAuth.Discord;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace LootCouncil.Presentation.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DiscordAuthenticationOptions>(_configuration.GetSection("DiscordAuthentication"));
            services.AddDbContext<LootCouncilDbContext>(cfg =>
            {
                cfg.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
                cfg.EnableDetailedErrors();
            });
            services.AddIdentityCore<LootCouncilUser>()
                .AddEntityFrameworkStores<LootCouncilDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllers();
            services.AddAuthentication()
                .AddJwtBearer(opt =>
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF32.GetBytes(_configuration["JwtBearer:Secret"])),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidAudience = _configuration["JwtBearer:Audience"],
                        ValidIssuer = _configuration["JwtBearer:Authority"]
                    }
                )
                .AddDiscord(opt =>
                {
                    opt.ClientId = _configuration["DiscordAuthentication:ClientId"];
                    opt.ClientSecret = _configuration["DiscordAuthentication:ClientSecret"];
                    opt.SignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddExternalCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}