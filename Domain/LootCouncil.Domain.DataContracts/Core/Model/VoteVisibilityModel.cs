using LootCouncil.Domain.DataContracts.Core.Request;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class VoteVisibilityModel
    {
        /// <summary>
        /// Allow all guild members to see vote results
        /// </summary>
        public bool AllowGuild { get; set; }
        /// <summary>
        /// Allow all eligible voters to see results for all items (even if they aren't voting).
        /// </summary>
        public bool AllowAllEligibleVoters { get; set; }
        /// <summary>
        /// Require voters submit their own vote
        /// </summary>
        public VoteSubmissionRequirement VoteSubmissionRequirement { get; set; }
    }
}