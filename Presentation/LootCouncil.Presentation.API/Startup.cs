using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine.DependencyInjection;
using LootCouncil.Presentation.API.Middleware;
using LootCouncil.Service.DependencyInjection;
using LootCouncil.Service.Mapping;
using LootCouncil.Utility.Configuration;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtTokenOptions>(_configuration.GetSection("JwtBearer"));
            services.AddDbContext<LootCouncilDbContext>(cfg =>
            {
                cfg.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
                cfg.EnableDetailedErrors();
            });
            services.AddIdentityCore<LootCouncilUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LootCouncilDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllers();
            services.AddAuthentication()
                .AddJwtBearer(opt =>
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Convert.FromBase64String(_configuration["JwtBearer:Secret"])),
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidAudience = _configuration["JwtBearer:Audience"],
                        ValidIssuer = _configuration["JwtBearer:Authority"]
                    }
                )
                .AddDiscord(opt =>
                {
                    opt.Scope.Add("guilds");
                    opt.ClientId = _configuration["DiscordAuthentication:ClientId"];
                    opt.ClientSecret = _configuration["DiscordAuthentication:ClientSecret"];
                    opt.SignInScheme = IdentityConstants.ExternalScheme;
                    opt.SaveTokens = true;
                })
                .AddExternalCookie();
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(GuildProfile).Assembly));
            services.AddCustomMiddleware();
            services.AddAuthorization();
            services.AddApplicationServices();
            services.AddApplicationEngines();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LootCouncilDbContext dbContext)
        {
            dbContext.Database.Migrate();
            app.UseExceptionHandling();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapControllers();
            });
        }
    }
}