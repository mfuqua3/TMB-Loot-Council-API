using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class WishlistDetailsModel : IOrdered
    {
        public int Order { get; set; }
    }
}