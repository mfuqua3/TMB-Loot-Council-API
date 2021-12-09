using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities;

public class PreVoteItemObjectionResponse : IUnique<int>, ITracked
{
    public int Id { get; set; }
    public int PreVoteItemObjectionId { get; set; }
    public PreVoteItemObjection PreVoteItemObjection { get; set; }
    public int PreVoteVoterId { get; set; }
    public PreVoteVoter PreVoteVoter { get; set; }
    public bool ResponseRequired { get; set; }
    public string Response { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}