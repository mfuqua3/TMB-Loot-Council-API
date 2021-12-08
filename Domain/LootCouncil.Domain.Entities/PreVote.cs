using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class PreVote : IUnique<int>, ITracked
    {
        public int Id { get; set; }
        public int GuildId { get; set; }
        public Guild Guild { get; set; }
        public int PreVoteConfigurationId { get; set; }
        public PreVoteConfiguration PreVoteConfiguration { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}