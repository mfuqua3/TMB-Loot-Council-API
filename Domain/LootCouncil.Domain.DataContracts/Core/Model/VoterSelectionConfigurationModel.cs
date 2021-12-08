using System.Collections.Generic;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class VoterSelectionConfigurationModel
    {
        /// <summary>
        /// Minimum number of voters required to produce a "valid" PreVote item
        /// </summary>
        public int MinimumVotersPerItem { get; set; }
        /// <summary>
        /// Maximum number of voters per item
        /// </summary>
        public int MaximumVotersPerItem { get; set; }
        /// <summary>
        /// GuildUser Ids for users who should be prioritized for every item (overrides Randomize).
        /// This does not override Conflict of Interest settings.
        /// </summary>
        public List<int> FixedVoters { get; set; }
        /// <summary>
        /// A whitelist of GuildUser Ids that should be considered for votes, regardless of their GuildRole
        /// </summary>
        public List<int> EligibleVoterIds { get; set; }
        /// <summary>
        /// A blacklist of GuildUser Ids that should not be considered for votes, regardless of their GuildRole
        /// </summary>
        public List<int> IneligibleVoterIds { get; set; }
        /// <summary>
        /// A list of GuildRoles that should automatically gain voter consideration (barring Whitelist/Blacklist considerations)
        /// </summary>
        public List<int> EligibleVoterRoles { get; set; }
        /// <summary>
        /// If true, the voters are randomly chosen for every item. Otherwise, the items will use a ranked priority from the list
        /// if provided eligible voters.
        /// </summary>
        public bool Randomize { get; set; }
    }
}