namespace LootCouncil.Domain.Entities
{
    public class DiscordIdentity
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get;set; }
        public LootCouncilUser User { get; set; }
    }
}