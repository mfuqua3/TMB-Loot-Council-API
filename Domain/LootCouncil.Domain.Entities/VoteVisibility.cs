using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class VoteVisibility: IUnique<int>
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
        public int VoteSubmissionRequirement { get; set; }

        public int Id { get; set; }
    }
}