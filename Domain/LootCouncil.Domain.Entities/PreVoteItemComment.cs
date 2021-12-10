using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteItemComment : IUnique<int>, ITracked
{
    public int Id { get; set; }
    public int PreVoteItemId { get; set; }
    public PreVoteItem PreVoteItem { get; set; }
    public PreVoteVoter PreVoteVoter { get; set; }
    public int PreVoteVoterId { get; set; }
    public string Comment { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}