using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine.Mapping
{
    public class PreVoteConfigurationProfile : Profile
    {
        public PreVoteConfigurationProfile()
        {
            CreateMap<ExpirationConfigurationModel, ExpirationConfiguration>()
                .ReverseMap();
            CreateMap<ItemSelectionConfigurationModel, ItemSelectionConfiguration>()
                .ReverseMap();
            CreateMap<VoterSelectionConfigurationModel, VoterSelectionConfiguration>()
                .ReverseMap();
            CreateMap<ConflictOfInterestConfigurationModel, ConflictOfInterestConfiguration>()
                .ReverseMap();
            CreateMap<TransparencyConfigurationModel, TransparencyConfiguration>()
                .ReverseMap();
            CreateMap<VoteVisibilityModel, VoteVisibility>()
                .ReverseMap();
        }
    }
}