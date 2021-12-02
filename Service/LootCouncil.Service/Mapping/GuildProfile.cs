using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            CreateMap<DiscordServerIdentity, DiscordServerResponse>()
                .ForMember(dto => dto.Configured,
                    o => o.MapFrom(x => x.GuildAssociation != null && x.GuildAssociation.Guild.Configuration != null));
            CreateMap<Guild, ClaimServerResponse>()
                .ForMember(dto => dto.Owner, o => o.MapFrom(entity => entity.Configuration.Owner));
        }
    }

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LootCouncilUser, UserSummaryResponse>();
        }
    }
}