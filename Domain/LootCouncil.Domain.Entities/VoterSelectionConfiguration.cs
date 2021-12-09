using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class VoterSelectionConfiguration: IUnique<int>
    {
        /// <summary>
        /// Minimum number of voters required to produce a "valid" PreVote item
        /// </summary>
        public int MinimumVotersPerItem { get; set; }

        /// <summary>
        /// Maximum number of voters per item
        /// </summary>
        public int MaximumVotersPerItem { get; set; }
        public List<GuildUserVoterConfiguration> VoterConfigurations { get; set; }

        public List<GuildRoleVoterConfiguration> EligibleRoleConfigurations { get; set; }

        public bool Randomize { get; set; }
        public int Id { get; set; }
    }
}