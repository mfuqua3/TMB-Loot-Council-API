using System.Collections.Generic;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class PreVoteItemModel
    {
        public int ItemId { get; set; }
        public List<EligibleCharacterModel> EligibleCharacters { get; set; }
        public List<VoterModel> Voters { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<ObjectionModel> Objections { get; set; }
        public bool CanObject { get; set; }
        public bool CanComment { get; set; }
    }
}