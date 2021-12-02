using System.Collections.Generic;

namespace LootCouncil.Utility.Configuration
{
    public class RootOptions
    {
        public List<RootUserConfiguration> Users { get; set; }
    }
    public class RootUserConfiguration
    {
        public string Email { get; set; }
        public DiscordIdentityConfiguration DiscordIdentity { get; set; }
    }
    public class DiscordIdentityConfiguration
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public string Discriminator { get; set; }
    }
}