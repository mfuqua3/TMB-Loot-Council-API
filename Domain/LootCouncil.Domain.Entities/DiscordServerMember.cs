using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class DiscordServerMember: IUnique<int>
    {
        public int Id { get; set; }
        public ulong MemberId { get; set; }
        public DiscordIdentity Member { get; set; }
        public ulong ServerId { get; set; }
        public DiscordServerIdentity Server { get; set; }
    }
}