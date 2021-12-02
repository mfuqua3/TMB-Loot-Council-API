using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IUserDataService
    {
        Task<List<GuildResponse>> GetUserGuilds(string userId);
    }
}