using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IGuildService
    {
        Task<ClaimServerResponse> ClaimDiscordServer(ClaimDiscordServerRequest request);
        Task<string> ChangeGuildScope(string userId, int id);
        Task ReleaseGuild(string userId, int guildId);
    }
}