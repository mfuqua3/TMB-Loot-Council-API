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

        async Task<ClaimGuildResponse> IGuildService.ClaimGuild(ClaimGuildRequest request)
        {
            var guild = await _dbContext.GuildUsers
                .Include(u => u.Guild)
                .ThenInclude(g => g.Configuration)
                .AsQueryable()
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.Guild)
                .SingleOrDefaultAsync(x => x.Id == request.GuildId);
            if (guild == null)
            {
                throw new KeyNotFoundException();
            }

            if (guild.Configuration?.OwnerId != null)
            {
                throw new InvalidOperationException("That guild has already been claimed.");
            }

            guild.Configuration ??= new GuildConfiguration();
            guild.Configuration.OwnerId = request.UserId;
            await _dbContext.SaveChangesAsync();
            return await _dbContext
                .Guilds
                .AsQueryable()
                .Where(x => x.Id == request.GuildId)
                .ProjectTo<ClaimGuildResponse>(_configurationProvider)
                .SingleAsync();
        }

        public async Task ReleaseGuild(string userId, ulong guildId)
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

        async Task<string> IGuildService.ChangeGuildScope(string userId, ulong id)
        {
            var user = await _dbContext.Users
                .Include(x => x.GuildUsers)
                .ThenInclude(x => x.Guild)
                .ThenInclude(x => x.Configuration)
                .SingleAsync(x => x.Id == userId);
            var guildUser = user.GuildUsers.FirstOrDefault(x => x.GuildId == id);
            if (guildUser == null)
            {
                throw new KeyNotFoundException();
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