using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IUserDataService
    {
        Task<List<DiscordServerResponse>> GetUserServers(string userId);
    }
}