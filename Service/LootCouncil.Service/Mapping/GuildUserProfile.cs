using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping
{
    public class GuildUserProfile :Profile
    {
        public GuildUserProfile()
        {
            CreateMap<GuildUser, GuildUserResponse>()
                .ForMember(x => x.DisplayName, o => o.MapFrom(x => x.User.UserName));
        }
    }
}