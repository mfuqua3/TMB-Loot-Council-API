using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class TransparencyConfiguration: IUnique<int>
    {
        public int VoteVisibilityId { get; set; }
        public VoteVisibility VoteVisibility { get; set; }
        public int Id { get; set; }
    }
}