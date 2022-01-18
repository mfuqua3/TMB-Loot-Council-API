using System;
using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class PreVoteItemObjection : IUnique<int>, ITracked
    {
        public int Id { get; set; }
        public int PreVoteVoterId { get; set; }
        public PreVoteVoter PreVoteVoter { get; set; }
        public int PreVoteItemId { get; set; }
        public PreVoteItem PreVoteItem { get; set; }
        public string Reason { get; set; }
        public List<PreVoteItemObjectionResponse> Responses { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}