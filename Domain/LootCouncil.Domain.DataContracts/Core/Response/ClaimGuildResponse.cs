namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class ClaimGuildResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public UserSummary Owner { get; set; }
    }
}