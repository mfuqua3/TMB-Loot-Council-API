using System.Text.Json.Serialization;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Core.Model;

namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class CreatePreVoteRequest: IUserScoped, IGuildScoped
    {
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public int GuildId { get; set; }
        public ExpirationConfigurationModel ExpirationConfiguration { get; set; }
        public ItemSelectionConfigurationModel ItemSelection { get; set; }
        public VoterSelectionConfigurationModel VoterSelection { get; set; }
        public ConflictOfInterestConfigurationModel ConflictOfInterest { get; set; }
        public TransparencyConfigurationModel Transparency { get; set; }
    }
}