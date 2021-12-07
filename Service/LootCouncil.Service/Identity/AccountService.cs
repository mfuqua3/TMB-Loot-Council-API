using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Identity.Model;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Service.Identity
{
    public class AccountService : IAccountService
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly IUserEngine _userEngine;
        private readonly UserManager<LootCouncilUser> _userManager;
        private readonly IJwtEngine _jwtEngine;

        public AccountService(LootCouncilDbContext dbContext,
            IUserEngine userEngine,
            UserManager<LootCouncilUser> userManager,
            IJwtEngine jwtEngine)
        {
            _dbContext = dbContext;
            _userEngine = userEngine;
            _userManager = userManager;
            _jwtEngine = jwtEngine;
        }

        async Task<Token> IAccountService.DiscordAuthorize(string accessToken)
        {
            using var client = new DiscordRestClient();
            await client.LoginAsync(TokenType.Bearer, accessToken);
            var discordUser = client.CurrentUser;
            var user = await _dbContext.Users
                           .Include(x=>x.DiscordIdentity)
                           .SingleOrDefaultAsync(x=>x.DiscordIdentity.Id == discordUser.Id) ??
                       await _userEngine.InitializeUserAsync(discordUser);
            var guildsPaginated = await client.GetGuildSummariesAsync().ToListAsync();
            await _userEngine.UpdateServersAsync(user.Id,
                guildsPaginated
                    .SelectMany(x => x)
                    .Cast<IUserGuild>()
                    .ToList());
            var token = await _jwtEngine.GenerateToken(user.Id);
            return new Token {AccessToken = token};
        }
    }
}