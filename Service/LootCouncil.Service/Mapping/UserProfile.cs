using AutoMapper;
using Discord;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LootCouncilUser, UserSummaryResponse>();
            CreateMap<ISelfUser, InitializeUserRequest>()
                .ForMember(x => x.DiscordUid, o => o.MapFrom(x => x.Id))
                .ForMember(x => x.DiscordDiscriminator, o => o.MapFrom(x => x.Discriminator));
        }
    }
}