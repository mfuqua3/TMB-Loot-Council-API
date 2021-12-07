using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildServerAssociation : IUnique<int>
    {
        public int Id { get; set; }
        public ulong ServerId { get; set; }
        public DiscordServerIdentity Server { get; set; }
        public int GuildId { get; set; }
        public Guild Guild { get; set; }
    }
}