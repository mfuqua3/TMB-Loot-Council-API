using System;
using System.Threading.Tasks;
using AutoMapper;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine
{
    public class PreVoteConfigurationEngine : IPreVoteConfigurationEngine
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IMapper _mapper;

        public PreVoteConfigurationEngine(LootCouncilDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrGetConfiguration(CreatePreVoteRequest request)
        {
            var configuration = new PreVoteConfiguration
            {
                ExpirationConfiguration =
                    await GetConfigOrDefault<ExpirationConfigurationModel, ExpirationConfiguration>(
                        request.ExpirationConfiguration),
                ItemSelectionConfiguration =
                    await GetConfigOrDefault<ItemSelectionConfigurationModel, ItemSelectionConfiguration>(
                        request.ItemSelection),
                VoterSelectionConfiguration =
                    await GetConfigOrDefault<VoterSelectionConfigurationModel, VoterSelectionConfiguration>(
                        request.VoterSelection),
                ConflictOfInterestConfiguration =
                    await GetConfigOrDefault<ConflictOfInterestConfigurationModel, ConflictOfInterestConfiguration>(
                        request.ConflictOfInterest),
                TransparencyConfiguration =
                    await GetConfigOrDefault<TransparencyConfigurationModel, TransparencyConfiguration>(
                        request.Transparency),
            };
            await _dbContext.PreVoteConfigurations.AddAsync(configuration);
            await _dbContext.SaveChangesAsync();
            return configuration.Id;
        }

        private async Task<TConfig> GetConfigOrDefault<TModel, TConfig>(TModel model)
            where TConfig : class, IUnique<int>
            where TModel : class
        {
            if (model == null)
            {
                return await _dbContext.Set<TConfig>().FindAsync(1);
            }

            return _mapper.Map<TModel, TConfig>(model);
        }
    }
}