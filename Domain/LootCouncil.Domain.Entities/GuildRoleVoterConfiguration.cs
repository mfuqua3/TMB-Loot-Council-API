using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildRoleVoterConfiguration: IUnique<int>
    {
        public int VoterSelectionConfigurationId { get; set; }
        public VoterSelectionConfiguration VoterSelectionConfiguration { get; set; }
        public int GuildRoleId { get; set; }
        public GuildRole GuildRole { get; set; }
        public int Id { get; set; }
    }
}