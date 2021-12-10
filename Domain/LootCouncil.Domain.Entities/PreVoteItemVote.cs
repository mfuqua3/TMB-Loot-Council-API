using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteItemVote : IUnique<int>, IOrdered, ITracked
{
    public int Id { get; set; }
    public int Order { get; set; }
    public int PreVoteItemId { get; set; }
    public PreVoteItem PreVoteItem { get; set; }
    public PreVoteCharacter PreVoteCharacter { get; set; }
    public int PreVoteCharacterId { get; set; }
    public PreVoteVoter PreVoteVoter { get; set; }
    public int PreVoteVoterId { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}