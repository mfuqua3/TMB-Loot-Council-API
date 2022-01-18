using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine
{
    public interface IUserEngine
    {
        Task<LootCouncilUser> InitializeUserAsync(InitializeUserRequest discordUser);
        Task UpdateServersAsync(UpdateServersRequest request);
    }
}