namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class ClaimGuildRequest
    {
        public string UserId { get; set; }
        public ulong GuildId { get; set; }
    }
}