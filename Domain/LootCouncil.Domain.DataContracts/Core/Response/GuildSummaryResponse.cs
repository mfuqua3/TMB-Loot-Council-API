namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class GuildSummaryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public UserSummaryResponse Owner { get; set; }
    }
}