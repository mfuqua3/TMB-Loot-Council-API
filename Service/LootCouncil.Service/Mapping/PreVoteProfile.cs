using System.Linq;
using AutoMapper;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Service.Mapping;

public class PreVoteProfile : Profile
{
    public PreVoteProfile()
    {
        CreateMap<PreVote, PreVoteDetail>()
            .ForMember(x => x.Expiration,
                o => o.MapFrom(x => x.PreVoteConfiguration.ExpirationConfiguration.ExpirationTime));
        CreateMap<PreVote, PreVoteSummary>()
            .ForMember(x => x.Expiration,
                o => o.MapFrom(x => x.PreVoteConfiguration.ExpirationConfiguration.ExpirationTime))
            .ForMember(x=>x.ItemCount, o=>o.MapFrom(x=>x.Items.Count));
        CreateMap<PreVoteItem, PreVoteItemModel>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Item.Name));
        CreateMap<PreVoteItemObjection, ObjectionModel>()
            .ForMember(o => o.TimeStamp, o => o.MapFrom(x => x.Created));
        CreateMap<PreVoteItemComment, CommentModel>()
            .ForMember(x => x.User, o => o.MapFrom(x => x.PreVoteVoter.GuildUser.User.UserName))
            .ForMember(x => x.TimeStamp, o => o.MapFrom(x => x.Updated ?? x.Created));
        CreateMap<PreVoteItemAssignment, VoterModel>()
            .ForMember(x => x.Id, o => o.MapFrom(x => x.PreVoteVoter.GuildUserId))
            .ForMember(x => x.Name, o => o.MapFrom(x => x.PreVoteVoter.GuildUser.User.UserName));
        CreateMap<PreVoteItemVote, VoteModel>()
            .ForMember(x => x.CharacterId, o => o.MapFrom(x => x.PreVoteCharacter.CharacterId));
        CreateMap<PreVoteCharacter, EligibleCharacterModel>()
            .ForMember(x => x.Name, o => o.MapFrom(x => x.Character.Name));
        CreateMap<PreVoteCharacterConsideration, CharacterConsiderationModel>();
    }
}