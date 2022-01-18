using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class VoteModel : IOrdered
    {
        public int CharacterId { get; set; }
        public int Order { get; set; }
    }
}