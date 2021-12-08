using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class ConflictOfInterestConfiguration: IUnique<int>
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

        public int Id { get; set; }
    }
}