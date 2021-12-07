using LootCouncil.Domain.DataContracts.ThatsMyBis;

namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class CreateImportRequest
    {
        public int GuildId { get; set; }
        public string UserId { get; set; }
        public TmbRosterState Data { get; set; }
    }
}