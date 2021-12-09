using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class ItemSelectionConfiguration: IUnique<int>
    {
        public bool SelectAll { get; set; }
        public int Id { get; set; }
    }
}