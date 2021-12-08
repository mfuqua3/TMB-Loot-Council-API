using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class PreVoteItemModel
    {
        public int ItemId { get; set; }
        public List<EligibleCharacterModel> EligibleCharacters { get; set; }
        public List<VoterModel> Voters { get; set; }
        public bool CanObject { get; set; }
        public bool CanComment { get; set; }
    }

    public class VoterModel
    {
        public int Id { get; set; } //GuildUserId
        public List<int> Votes { get; set; }
    }
    public class EligibleCharacterModel
    {
        public int CharacterId { get; set; }
        public WishlistDetailsModel WishlistDetails { get; set; }
    }

    public class WishlistDetailsModel : IOrdered
    {
        public int Order { get; set; }
    }
}