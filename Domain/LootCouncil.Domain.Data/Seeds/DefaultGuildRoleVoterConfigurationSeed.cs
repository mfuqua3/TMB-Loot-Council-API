using LootCouncil.Domain.Entities;

namespace LootCouncil.Domain.Data.Seeds
{
    public class DefaultGuildRoleVoterConfigurationSeed : DataSeedConfiguration<GuildRoleVoterConfiguration>
    {
        protected override GuildRoleVoterConfiguration[] Data => new[]
        {
            new GuildRoleVoterConfiguration()
            {
                Id = 1,
                VoterSelectionConfigurationId = 1,
                GuildRoleId = 1,
            }
        };
    }
}