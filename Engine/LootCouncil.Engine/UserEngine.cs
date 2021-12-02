using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Engine
{
    public class UserEngine : IUserEngine
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly UserManager<LootCouncilUser> _userManager;

        public UserEngine(LootCouncilDbContext dbContext, UserManager<LootCouncilUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<LootCouncilUser> InitializeUserAsync(ISelfUser discordUser)
        {
            var newUser = new LootCouncilUser()
            {
                Email = discordUser.Email,
                UserName = discordUser.Username,
                DiscordIdentity = new DiscordIdentity()
                {
                    Id = discordUser.Id,
                    UserName = discordUser.Username,
                    Discriminator = discordUser.Discriminator
                },
                GuildUsers = new List<GuildUser>()
            };
            await _userManager.CreateAsync(newUser);
            await _userManager.AddToRoleAsync(newUser, AuthorizationConstants.Roles.Basic);
            return newUser;
        }

        public async Task UpdateGuildsAsync(string userId, ICollection<IUserGuild> guilds)
        {
            var user = await _dbContext.Users
                .Include(x => x.GuildUsers)
                .ThenInclude(x => x.Guild)
                .SingleAsync(x => x.Id == userId);
            var sourceGuildIds = guilds.Select(g => g.Id).ToArray();
            var destinationGuildIds = user.GuildUsers.Select(g => g.GuildId).ToArray();
            var toRemove = user.GuildUsers.Where(x => !sourceGuildIds.Contains(x.GuildId));
            var toAdd = guilds.Where(x => !destinationGuildIds.Contains(x.Id));
            _dbContext.GuildUsers.RemoveRange(toRemove);
            foreach (var guild in toAdd)
            {
                var guildEntity = await AddOrGetGuild(guild);
                var guildUser = new GuildUser()
                {
                    UserId = userId,
                    GuildId = guildEntity.Id
                };
                await _dbContext.GuildUsers.AddAsync(guildUser);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task<Guild> AddOrGetGuild(IUserGuild guild)
        {
            var existingGuild = await EntityFrameworkQueryableExtensions.SingleOrDefaultAsync(_dbContext.Guilds, x => x.Id == guild.Id);
            if (existingGuild != null) return existingGuild;
            var newGuild = new Guild()
            {
                Name = guild.Name,
                Id = guild.Id
            };
            await _dbContext.Guilds.AddAsync(newGuild);
            await _dbContext.SaveChangesAsync();
            return newGuild;
        }
    }
}