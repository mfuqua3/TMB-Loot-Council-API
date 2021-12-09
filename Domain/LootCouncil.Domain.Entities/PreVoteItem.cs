using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteItem : IUnique<int>
{
    public int Id { get; set; }
    public int PreVoteId { get; set; }
    public PreVote PreVote { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }
    public List<PreVoteItemAssignment> VoterAssignments { get; set; }
    public List<PreVoteItemVote> Votes { get; set; }
    public List<PreVoteItemComment> Comments { get; set; }
    public List<PreVoteItemObjection> Objections { get; set; }
    public List<PreVoteCharacter> EligibleCharacters { get; set; }
}