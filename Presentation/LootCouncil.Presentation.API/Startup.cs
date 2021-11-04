using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine.DependencyInjection;
using LootCouncil.Service.DependencyInjection;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtTokenOptions>(_configuration.GetSection("JwtToken"));
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
                            Encoding.UTF32.GetBytes(_configuration["JwtToken:Secret"])),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        ValidAudience = _configuration["JwtToken:Audience"],
                        ValidIssuer = _configuration["JwtToken:Authority"]
                    }
                )
                .AddDiscord(opt =>
                {
                    opt.ClientId = _configuration["DiscordAuthentication:ClientId"];
                    opt.ClientSecret = _configuration["DiscordAuthentication:ClientSecret"];
                    opt.SignInScheme = IdentityConstants.ExternalScheme;
                    opt.Scope.Add("guilds");
                })
                .AddExternalCookie();
            services.AddAuthorization();
            services.AddApplicationServices();
            services.AddApplicationEngines();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LootCouncilDbContext dbContext)
        {
            dbContext.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}