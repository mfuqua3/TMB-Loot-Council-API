using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteVoter : IUnique<int>
{
    public int Id { get; set; }
    public int PreVoteId { get; set; }
    public PreVote PreVote { get; set; }
    public int GuildUserId { get; set; }
    public GuildUser GuildUser { get; set; }
    public List<PreVoteItemAssignment> ItemAssignments { get; set; }
    public List<PreVoteItemVote> Votes { get; set; }
    public List<PreVoteItemComment> Comments { get; set; }
    public List<PreVoteItemObjection> Objections { get; set; }
}