namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class DiscordServerResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public GuildSummaryResponse Guild { get; set; }
    }
}