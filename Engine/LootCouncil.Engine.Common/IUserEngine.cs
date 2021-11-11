using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine
{
    public interface IUserEngine
    {
        Task<LootCouncilUser> InitializeUserAsync(ISelfUser discordUser);
        Task UpdateGuildsAsync(string userId, ICollection<IUserGuild> guilds);
    }
}