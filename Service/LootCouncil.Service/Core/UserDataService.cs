using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Response;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Service.Core
{
    public class UserDataService : IUserDataService
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IConfigurationProvider _mapperConfigurationProvider;

        public UserDataService(LootCouncilDbContext dbContext, IConfigurationProvider mapperConfigurationProvider)
        {
            _dbContext = dbContext;
            _mapperConfigurationProvider = mapperConfigurationProvider;
        }
        public async Task<List<GuildResponse>> GetUserGuilds(string userId)
        {
            return await _dbContext.GuildUsers
                .AsQueryable()
                .Where(x => x.UserId == userId)
                .Select(x => x.Guild)
                .ProjectTo<GuildResponse>(_mapperConfigurationProvider)
                .ToListAsync();
        }
    }
}