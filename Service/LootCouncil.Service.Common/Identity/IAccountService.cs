using System.Security.Claims;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Identity.Response;

namespace LootCouncil.Service.Identity
{
    public interface IAccountService
    {
        Task<TokenResponse> DiscordAuthorize(string discordAccessToken);
    }
}