using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            CreateMap<Guild, GuildResponse>()
                .ForMember(dto => dto.Configured, o => o.MapFrom(entity => entity.Configuration != null));
            CreateMap<Guild, ClaimGuildResponse>()
                .ForMember(dto => dto.Owner, o => o.MapFrom(entity => entity.Configuration.Owner));
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LootCouncilUser, UserSummary>();
        }
    }
}