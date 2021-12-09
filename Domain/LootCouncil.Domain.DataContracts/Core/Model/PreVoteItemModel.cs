using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class PreVoteItemModel : INamed
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public List<EligibleCharacterModel> EligibleCharacters { get; set; }
        public List<VoterModel> VoterAssignments { get; set; }
        public List<VoteModel> Votes { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<ObjectionModel> Objections { get; set; }
    }
}