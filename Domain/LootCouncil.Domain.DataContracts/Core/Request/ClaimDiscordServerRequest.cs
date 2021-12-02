namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class ClaimDiscordServerRequest
    {
        public string UserId { get; set; }
        public ulong ServerId { get; set; }
    }
}