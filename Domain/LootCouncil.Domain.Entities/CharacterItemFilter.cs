using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class CharacterItemFilter:IUnique<int>
    {
        public int Id { get; set; }
        public int CharacterItemId { get; set; }
        public CharacterItem CharacterItem { get; set; }
        public int ItemFilterId { get; set; }
        public ItemFilter ItemFilter { get; set; }
    }
}