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

        public async Task UpdateServersAsync(string userId, ICollection<IUserGuild> servers)
        {
            var userDiscordIdentity = await _dbContext.Users
                .AsQueryable()
                .Include(x=>x.DiscordIdentity)
                .ThenInclude(x=>x.ServerMemberships)
                .Where(x => x.Id == userId)
                .Select(x => x.DiscordIdentity)
                .SingleAsync();
            var sourceGuildIds = servers.Select(g => g.Id).ToArray();
            var destinationGuildIds = userDiscordIdentity.ServerMemberships.Select(x => x.ServerId);
            var toRemove = userDiscordIdentity.ServerMemberships.Where(x => !sourceGuildIds.Contains(x.ServerId));
            var toAdd = servers.Where(x => !destinationGuildIds.Contains(x.Id));
            _dbContext.DiscordServerMembers.RemoveRange(toRemove);
            foreach (var guild in toAdd)
            {
                var serverEntity = await AddOrGetServer(guild);
                var serverMember = new DiscordServerMember()
                {
                    MemberId = userDiscordIdentity.Id,
                    ServerId = serverEntity.Id
                };
                await _dbContext.DiscordServerMembers.AddAsync(serverMember);
            }

            await _dbContext.SaveChangesAsync();
        }

        private async Task<DiscordServerIdentity> AddOrGetServer(IUserGuild guild)
        {
            var existingServer = await _dbContext.DiscordServers.FindAsync(guild.Id);
            if (existingServer != null) return existingServer;
            var newServer = new DiscordServerIdentity()
            {
                Name = guild.Name,
                Id = guild.Id
            };
            await _dbContext.DiscordServers.AddAsync(newServer);
            await _dbContext.SaveChangesAsync();
            return newServer;
        }
    }
}