using System.Collections.Generic;

namespace LootCouncil.Domain.Entities
{
    public class Guild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public List<GuildUser> GuildUsers { get; set; }
        public GuildConfiguration Configuration { get; set; }
    }
}