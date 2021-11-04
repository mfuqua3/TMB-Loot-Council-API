using Microsoft.AspNetCore.Identity;

namespace LootCouncil.Domain.Entities
{
    public class LootCouncilUser : IdentityUser
    {
        public long? DiscordIdentityId { get; set; }
        public DiscordIdentity DiscordIdentity { get; set; }
    }
}