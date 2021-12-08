namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class ConflictOfInterestConfigurationModel
    {
        /// <summary>
        /// Can vote for themselves
        /// </summary>
        public bool AllowSelfVoting { get; set; }
        /// <summary>
        /// Can vote at all
        /// </summary>
        public bool AllowVoting { get; set; }
        /// <summary>
        /// Can object to current vote state
        /// </summary>
        public bool AllowObjecting { get; set; }
        /// <summary>
        /// Can comment on an item
        /// </summary>
        public bool AllowCommenting { get; set; }
    }
}