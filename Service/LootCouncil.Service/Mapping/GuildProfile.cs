using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class GuildProfile : Profile
    {
        public GuildProfile()
        {
            CreateMap<Guild, GuildSummaryResponse>()
                .ForMember(dto => dto.Owner, o => o.MapFrom(entity => entity.Configuration.Owner));
            CreateMap<DiscordServerIdentity, DiscordServerResponse>()
                .ForMember(dto => dto.Guild,
                    o => o.MapFrom(x => x.GuildAssociation.Guild));
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