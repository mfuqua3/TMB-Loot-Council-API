using System.Security.Claims;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.DataContracts.Identity.Model;
using LootCouncil.Domain.Entities;
using LootCouncil.Engine;
using Microsoft.AspNetCore.Identity;

namespace LootCouncil.Service.Identity
{
    public class AccountService : IAccountService
    {
        private readonly LootCouncilDbContext _dbContext;
        private readonly UserManager<LootCouncilUser> _userManager;
        private readonly IJwtEngine _jwtEngine;

        public AccountService(LootCouncilDbContext dbContext,
            UserManager<LootCouncilUser> userManager,
            IJwtEngine jwtEngine)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _jwtEngine = jwtEngine;
        }
        async Task<Token> IAccountService.DiscordAuthorize(ClaimsPrincipal claims)
        {
            var id = long.Parse(claims.FindFirstValue(ClaimTypes.NameIdentifier));
            var discordIdentity = await _dbContext.DiscordIdentities.FindAsync(id);
            LootCouncilUser user;
            if (discordIdentity == null)
            {
                user = new LootCouncilUser()
                {
                    Email = claims.FindFirstValue(ClaimTypes.Email),
                    UserName = claims.FindFirstValue(ClaimTypes.Name),
                    DiscordIdentity = new DiscordIdentity()
                    {
                        UserName = claims.FindFirstValue(ClaimTypes.Name),
                        Id = id
                    }
                };
                await _userManager.CreateAsync(user);
                discordIdentity = user.DiscordIdentity;
            }
            await _dbContext.Entry(discordIdentity).Reference(x=>x.User).LoadAsync();
            user = discordIdentity.User;
            var tokenString = _jwtEngine.GenerateToken(user);
            return new Token()
            {
                AccessToken = tokenString
            };
        }
    }
}