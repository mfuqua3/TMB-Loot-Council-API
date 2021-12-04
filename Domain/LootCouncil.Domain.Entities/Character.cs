using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.Entities
{
    public class Character : IUnique<int>, INamed
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Spec { get; set; }
        public List<CharacterItem> CharacterItems { get; set; }
    }
}