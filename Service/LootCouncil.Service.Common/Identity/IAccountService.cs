using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Identity.Model;

namespace LootCouncil.Service.Identity
{
    public interface IAccountService
    {
        Task<Token> DiscordAuthorize(string discordAccessToken);
    }
}