using System;
using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class CharacterItem : IUnique<int>, ITracked
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public string Type { get; set; } //Wishlist, Award, etc...
        public int? Order { get; set; }
        public DateTime? Date { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public List<CharacterItemFilter> CharacterItemFilters { get; set; }
    }
}