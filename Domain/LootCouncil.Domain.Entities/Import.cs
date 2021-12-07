using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class Import : IUnique<int>, ITracked
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
        public bool Faulted { get; set; }
        public string Error { get; set; }
        public int GuildId { get; set; }
        public Guild Guild { get; set; }
        public string UserId { get; set; }
        public double Progress { get; set; }
        public LootCouncilUser User { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}