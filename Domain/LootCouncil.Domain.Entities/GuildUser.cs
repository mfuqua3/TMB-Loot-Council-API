using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildUser: IUnique<int>
    {
        public string UserId { get; set; }
        public LootCouncilUser User { get; set; }
        public Guild Guild { get; set; }
        public ulong GuildId { get; set; }
        public int Id { get; set; }
    }
}