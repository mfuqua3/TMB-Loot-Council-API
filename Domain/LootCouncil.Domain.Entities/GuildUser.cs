using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildUser: IUnique<int>
    {
        public string UserId { get; set; }
        public LootCouncilUser User { get; set; }
        public Guild Guild { get; set; }
        public int GuildId { get; set; }
        public int Id { get; set; }
        public int RoleId { get; set; }
        public GuildRole Role { get; set; }
        public List<Character> Characters { get; set; }
    }
}