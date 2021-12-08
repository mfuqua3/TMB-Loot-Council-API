using System;
using Hangfire;
using Hangfire.PostgreSql;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine.DependencyInjection;
using LootCouncil.Presentation.API.BackgroundServices;
using LootCouncil.Presentation.API.Middleware;
using LootCouncil.Service.DependencyInjection;
using LootCouncil.Service.Mapping;
using LootCouncil.Utility.Configuration;
using LootCouncil.Utility.Converters;
using LootCouncil.Utility.Wowhead;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.Configure<JwtTokenOptions>(_configuration.GetSection("JwtBearer"));
            services.Configure<RootOptions>(_configuration.GetSection("Root"));
            services.AddDbContextFactory<LootCouncilDbContext>(cfg =>
            {
                cfg.UseNpgsql(connectionString);
                cfg.EnableDetailedErrors();
            });
            services.AddDbContext<LootCouncilDbContext>(cfg =>
            {
                cfg.UseNpgsql(connectionString);
                cfg.EnableDetailedErrors();
            });
            services.AddIdentityCore<LootCouncilUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LootCouncilDbContext>()
                .AddDefaultTokenProviders();
            services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    //opt.JsonSerializerOptions.Converters.Add(new NumberToBooleanConverter());
                });
            services.AddHealthChecks();
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
            services.AddAutoMapper(cfg =>
                cfg.AddMaps(
                    typeof(ServicesContainerRegistrar).Assembly, 
                    typeof(EnginesContainerRegistrar).Assembly));
            services.AddCustomMiddleware();
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() {Title = "TMB LootCouncil", Version = "v1"});
            });
            services.AddHangfire((cfg) =>
            {
                cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
                cfg.UseSimpleAssemblyNameTypeSerializer();
                cfg.UseRecommendedSerializerSettings();
                cfg.UsePostgreSqlStorage(connectionString);
                cfg.UseFilter(new AutomaticRetryAttribute { Attempts = 0 });
            });
            services.AddHangfireServer();
            services.AddHostedService<IdentitySeeder>();
            services.AddTransient<IWowheadClient, WowheadClient>();
            services.AddApplicationServices();
            services.AddApplicationEngines();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LootCouncilDbContext dbContext)
        {
            dbContext.Database.Migrate();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TMB LootCouncil v1");
                c.RoutePrefix = "swagger";
            });
            app.UseExceptionHandling();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(c=>c
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapHangfireDashboard();
                endpoints.MapControllers();
            });
        }
    }
}