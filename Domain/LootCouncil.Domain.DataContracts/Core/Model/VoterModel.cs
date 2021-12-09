using System.Collections.Generic;
using LootCouncil.Domain.Data;

namespace LootCouncil.Domain.DataContracts.Core.Model
{
    public class VoterModel : INamed
    {
        public int Id { get; set; } //GuildUserId
        public string Name { get; set; }
    }
}