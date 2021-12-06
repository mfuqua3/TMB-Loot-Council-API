using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Service.Core
{
    public class GuildService : IGuildService
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IJwtEngine _jwtEngine;

        public GuildService(LootCouncilDbContext dbContext, IConfigurationProvider configurationProvider,
            IJwtEngine jwtEngine)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
            _jwtEngine = jwtEngine;
        }

        async Task<ClaimServerResponse> IGuildService.ClaimDiscordServer(ClaimDiscordServerRequest request)
        {
            var discordServer = await _dbContext.DiscordServers.FindAsync(request.ServerId);
            if (discordServer == null)
            {
                throw new KeyNotFoundException();
            }

            var guild = await _dbContext.GuildUsers
                .Include(u => u.Guild)
                .ThenInclude(g => g.Configuration)
                .Include(u => u.Guild)
                .ThenInclude(g => g.ServerAssociation)
                .AsQueryable()
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.Guild)
                .SingleOrDefaultAsync(x => x.ServerAssociation.ServerId == request.ServerId);
            if (guild?.Configuration?.OwnerId != null)
            {
                throw new InvalidOperationException("That guild has already been claimed.");
            }

            if (guild == null)
            {
                guild = new Guild()
                {
                    Name = discordServer.Name,
                    ServerAssociation = new GuildServerAssociation
                    {
                        ServerId = request.ServerId,
                    },
                    Configuration = new GuildConfiguration()
                };
                await _dbContext.Guilds.AddAsync(guild);
            }

            guild.Configuration ??= new GuildConfiguration();
            guild.Configuration.OwnerId = request.UserId;
            await _dbContext.SaveChangesAsync();
            return await _dbContext
                .Guilds
                .AsQueryable()
                .Where(x => x.ServerAssociation.ServerId == request.ServerId)
                .ProjectTo<ClaimServerResponse>(_configurationProvider)
                .SingleAsync();
        }

        public async Task ReleaseGuild(string userId, int guildId)
        {
            var guild = await _dbContext.GuildUsers
                .Include(u => u.Guild)
                .ThenInclude(g => g.Configuration)
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.Guild)
                .SingleOrDefaultAsync(x => x.Id == guildId);
            if (guild == null)
            {
                throw new KeyNotFoundException();
            }

            if (guild.Configuration == null || guild.Configuration?.OwnerId != userId)
            {
                throw new InvalidOperationException("That guild is not owned by that user.");
            }

            guild.Configuration.OwnerId = null;
            await _dbContext.SaveChangesAsync();
        }

        async Task<string> IGuildService.ChangeGuildScope(string userId, int id)
        {
            var guild = await _dbContext.Guilds.FindAsync(id);
            if (guild == null)
            {
                throw new KeyNotFoundException("No guild by that ID exists");
            }

            var user = await _dbContext.Users
                .Include(x => x.DiscordIdentity)
                .ThenInclude(x => x.ServerMemberships)
                .Include(x => x.GuildUsers)
                .ThenInclude(x => x.Guild)
                .ThenInclude(x => x.Configuration)
                .SingleAsync(x => x.Id == userId);
            var guildUser = user.GuildUsers.FirstOrDefault(x => x.GuildId == id);
            if (guildUser == null)
            {
                await _dbContext.Entry(guild).Reference(x => x.ServerAssociation).LoadAsync();
                var isInServer =
                    user.DiscordIdentity.ServerMemberships.Any(x => x.ServerId == guild.ServerAssociation.ServerId);
                //TODO
                throw new NotImplementedException();
            }

            if (guildUser.Guild.Configuration == null)
            {
                throw new InvalidOperationException("That guild has not been configured.");
            }

            user.ActiveGuildId = id;
            await _dbContext.SaveChangesAsync();
            return await _jwtEngine.GenerateToken(userId);
        }
    }
}