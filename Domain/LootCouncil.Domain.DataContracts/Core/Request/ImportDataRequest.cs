using LootCouncil.Domain.DataContracts.ThatsMyBis;

namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class ImportTmbDataRequest
    {
        public ulong GuildId { get; set; }
        public TmbRosterState Data { get; set; }
    }
}