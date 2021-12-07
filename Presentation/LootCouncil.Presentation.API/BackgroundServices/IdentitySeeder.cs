using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Authorization;
using LootCouncil.Utility.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LootCouncil.Presentation.API.BackgroundServices
{
    public class IdentitySeeder : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<IdentitySeeder> _logger;
        private readonly List<RootUserConfiguration> _rootUsers;

        private readonly string[] _roles =
        {
            AuthorizationConstants.Roles.Basic,
            AuthorizationConstants.Roles.Developer,
            AuthorizationConstants.Roles.Admin,
        };

        public IdentitySeeder(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<IdentitySeeder> logger,
            IOptions<RootOptions> rootOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _rootUsers = rootOptions.Value.Users;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var workerScope = _serviceScopeFactory.CreateScope();
                var dbContext = workerScope.ServiceProvider.GetRequiredService<LootCouncilDbContext>();
                var roleManager = workerScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = workerScope.ServiceProvider.GetRequiredService<UserManager<LootCouncilUser>>();
                await dbContext.Database.MigrateAsync(stoppingToken);
                await EnsureRolesCreated(roleManager, stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
                var rootUsers = await EnsureRootUsersCreated(dbContext, userManager, stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
                await EnsureRootAccess(rootUsers, userManager, stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("An unexpected error occured while attempt to initialize root users.", ex);
                throw;
            }
        }

        private async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager, CancellationToken stoppingToken)
        {
            foreach (var role in _roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                {
                    continue;
                }

                var identityRole = new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpper()
                };
                var result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"{identityRole.NormalizedName} role has been seeded successfully.");
                }
                else
                {
                    _logger.LogError($"Error while seeding {identityRole.NormalizedName} role.\n " +
                                     $"Detail => {result.Errors}");
                }
            }
        }

        private async Task<IEnumerable<LootCouncilUser>> EnsureRootUsersCreated(LootCouncilDbContext dbContext,
            UserManager<LootCouncilUser> userManager,
            CancellationToken stoppingToken)
        {
            var rootUsers = new List<LootCouncilUser>();
            var configuredUsers = _rootUsers;
            if (_rootUsers == null)
            {
                return rootUsers;
            }
            foreach (var configuredUser in configuredUsers)
            {
                var user = await dbContext.Users
                    .Include(x => x.DiscordIdentity)
                    .Where(x => x.DiscordIdentity.Id == configuredUser.DiscordIdentity.Id)
                    .SingleOrDefaultAsync(stoppingToken);
                if (user == null)
                {
                    user = new LootCouncilUser()
                    {
                        Email = configuredUser.Email,
                        UserName = configuredUser.DiscordIdentity.UserName,
                        DiscordIdentity = new DiscordIdentity()
                        {
                            Id = configuredUser.DiscordIdentity.Id,
                            UserName = configuredUser.DiscordIdentity.UserName,
                            Discriminator = configuredUser.DiscordIdentity.Discriminator
                        }
                    };
                    var result = await userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"A new root user has been created ({user.UserName})");
                    }
                    else
                    {
                        _logger.LogError(
                            $"An error occurred while attempting to seed a new root user ({user.UserName})\n" +
                            $"Error => {result.Errors}");
                    }
                }

                rootUsers.Add(user);
            }

            return rootUsers;
        }

        private async Task EnsureRootAccess(IEnumerable<LootCouncilUser> rootUsers,
            UserManager<LootCouncilUser> userManager, CancellationToken stoppingToken)
        {
            foreach (var user in rootUsers)
            {
                var roles = await userManager.GetRolesAsync(user);
                var toAdd = _roles.Except(roles).ToList();
                if (toAdd.Any())
                {
                    await userManager.AddToRolesAsync(user, toAdd);
                }
            }
        }
    }
}