using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class ItemFilter: IUnique<int>, INamed
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}