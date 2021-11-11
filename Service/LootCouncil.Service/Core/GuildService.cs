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
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Service.Core
{
    public class GuildService : IGuildService
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IConfigurationProvider _configurationProvider;

        public GuildService(LootCouncilDbContext dbContext, IConfigurationProvider configurationProvider)
        {
            _dbContext = dbContext;
            _configurationProvider = configurationProvider;
        }

        public async Task<ClaimGuildResponse> ClaimGuild(ClaimGuildRequest request)
        {
            var guild = await _dbContext.GuildUsers
                .AsQueryable()
                .Where(x => x.UserId == request.UserId)
                .Select(x => x.Guild)
                .Include(x => x.Configuration)
                .SingleOrDefaultAsync(x => x.Id == request.GuildId);
            if (guild == null)
            {
                throw new KeyNotFoundException();
            }
            if (guild.Configuration != null)
            {
                throw new InvalidOperationException("That guild has already been claimed.");
            }
            guild.Configuration = new GuildConfiguration()
            {
                OwnerId = request.UserId
            };
            await _dbContext.SaveChangesAsync();
            return await _dbContext
                .Guilds
                .AsQueryable()
                .Where(x => x.Id == request.GuildId)
                .ProjectTo<ClaimGuildResponse>(_configurationProvider)
                .SingleAsync();
        }
    }
}