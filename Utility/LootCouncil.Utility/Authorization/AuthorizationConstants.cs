namespace LootCouncil.Utility.Authorization
{
    public static class AuthorizationConstants
    {
        public static class Roles
        {
            public const string Basic = "Basic";
            public const string Admin = "Admin";
            public const string Developer = "Developer";
        }

        public static class Claims
        {
            public const string GuildId = "guild_id";
            public const string GuildName = "guild_name";
            public const string GuildRole = "guild_role";
        }
    }
}