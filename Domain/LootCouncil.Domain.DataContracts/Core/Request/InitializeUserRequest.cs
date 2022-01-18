using System.Collections.Generic;

namespace LootCouncil.Domain.DataContracts.Core.Request
{
    public class InitializeUserRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public ulong DiscordUid { get; set; }
        public string DiscordDiscriminator { get; set; }
    }

    public class UpdateServersRequest
    {
        public string UserId { get; set; }
        public List<ServerModel> Servers { get; set; }
    }

    public class ServerModel
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
    }
}