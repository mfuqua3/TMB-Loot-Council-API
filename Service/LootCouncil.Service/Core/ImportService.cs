using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hangfire;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LootCouncil.Service.Core
{
    public class ImportService : IImportService
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly LootCouncilDbContext _dbContext;
        private readonly ILogger<ImportService> _logger;

        public ImportService(
            IConfigurationProvider configurationProvider,
            LootCouncilDbContext dbContext,
            ILogger<ImportService> logger)
        {
            _configurationProvider = configurationProvider;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ImportResponse> CreateImportAsync(CreateImportRequest request)
        {
            var import = new Import()
            {
                GuildId = request.GuildId,
                UserId = request.UserId
            };
            await _dbContext.AddAsync(import);
            await _dbContext.SaveChangesAsync();
            BackgroundJob.Enqueue<IThatsMyBisDataEngine>(x => x.ImportData(import.Id, request.Data));
            return await _dbContext
                .Imports
                .ProjectTo<ImportResponse>(_configurationProvider)
                .SingleAsync(x => x.Id == import.Id);
        }

        public async Task<ImportResponse> GetImportAsync(int importId, int guildId)
        {
            try
            {
                return await _dbContext
                    .Imports
                    .AsQueryable()
                    .Where(x => x.GuildId == guildId)
                    .ProjectTo<ImportResponse>(_configurationProvider)
                    .SingleAsync(x => x.Id == importId);
            }
            catch (InvalidOperationException ex)
            {
                throw new KeyNotFoundException("Element not found. See inner exception for details.", ex);
            }
        }
    }
}