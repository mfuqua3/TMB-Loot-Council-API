using System;
using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class DiscordIdentity : IUnique<ulong>, ITracked
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public LootCouncilUser User { get; set; }
        public string Discriminator { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<DiscordServerMember> ServerMemberships { get; set; }
    }
}