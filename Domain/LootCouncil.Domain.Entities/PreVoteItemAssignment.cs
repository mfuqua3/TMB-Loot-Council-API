using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteItemAssignment:IUnique<int>
{
    public int Id { get; set; }
    public PreVoteVoter PreVoteVoter { get; set; }
    public int PreVoteVoterId { get; set; }
    public PreVoteItem PreVoteItem { get; set; }
    public int PreVoteItemId { get; set; }
}