using System.Collections.Generic;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class VoterModel
    {
        public int Id { get; set; } //GuildUserId
        public List<int> Votes { get; set; }
    }
}