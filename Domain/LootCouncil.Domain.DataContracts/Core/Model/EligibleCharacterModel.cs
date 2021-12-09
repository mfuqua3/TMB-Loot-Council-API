using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class EligibleCharacterModel : INamed
    {
        public int CharacterId { get; set; }

        public string Name { get; set; }
        public List<CharacterConsiderationModel> CharacterConsiderations { get; set; }
    }
}