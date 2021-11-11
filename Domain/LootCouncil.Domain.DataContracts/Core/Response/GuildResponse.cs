namespace LootCouncil.Domain.DataContracts.Core.Response
{
    public class GuildResponse
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public bool Configured { get; set; }
    }
}