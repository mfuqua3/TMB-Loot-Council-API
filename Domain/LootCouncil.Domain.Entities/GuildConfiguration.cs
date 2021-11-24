using System;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildConfiguration : IUnique<int>, ITracked
    {
        public int Id { get; set; }
        public Guild Guild { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string OwnerId { get; set; }
        public LootCouncilUser Owner { get; set; }
    }
}