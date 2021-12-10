using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class GuildUserVoterConfiguration: IUnique<int>
    {
        public int VoterSelectionConfigurationId { get; set; }
        public VoterSelectionConfiguration VoterSelectionConfiguration { get; set; }
        public int GuildUserId { get; set; }
        public GuildUser GuildUser { get; set; }
        public bool Eligible { get; set; }
        public bool Fixed { get; set; }
        public int Id { get; set; }
    }
}