using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class DiscordServerIdentity : IUnique<ulong>, INamed
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public GuildServerAssociation GuildAssociation { get; set; }
        public List<DiscordServerMember> Members { get; set; }
    }
}