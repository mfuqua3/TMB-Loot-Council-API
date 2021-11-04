namespace LootCouncil.Utility.Configuration
{
    public class JwtTokenOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TtlMinutes { get; set; }
    }
}