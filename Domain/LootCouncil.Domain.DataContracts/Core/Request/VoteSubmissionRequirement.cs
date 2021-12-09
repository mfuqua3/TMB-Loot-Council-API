namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public enum VoteSubmissionRequirement
    {
        None,
        /// <summary>
        /// A user may not see other votes until their own vote has been submitted
        /// </summary>
        RequireOwnVote,
        /// <summary>
        /// No votes will be visible until all have been submitted
        /// </summary>
        RequireAllVotes
    }
}