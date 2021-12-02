using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Authorization;
using LootCouncil.Utility.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LootCouncil.Engine
{
    internal class JwtEngine : IJwtEngine
    {
        private readonly UserManager<LootCouncilUser> _userManager;
        private readonly LootCouncilDbContext _db;
        private readonly JwtTokenOptions _config;

        public JwtEngine(
            IOptions<JwtTokenOptions> options, 
            UserManager<LootCouncilUser> userManager,
            LootCouncilDbContext db)
        {
            _userManager = userManager;
            _db = db;
            _config = options.Value;
        }
        public async Task<string> GenerateToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            await _db.Entry(user).Reference(x => x.ActiveGuild).LoadAsync();
            if(user.ActiveGuild!=null)
            {
                await _db.Entry(user.ActiveGuild).Reference(x => x.Configuration).LoadAsync();
            }
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            });
            claims.AddClaims(roles.Select(x => new Claim(ClaimTypes.Role, x)));
            if (user.ActiveGuild != null)
            {
                var role = user.ActiveGuild.Configuration.OwnerId == userId
                    ? "Owner"
                    : AuthorizationConstants.Roles.Basic;
                claims.AddClaims(new[]
                {
                    new Claim(AuthorizationConstants.Claims.GuildId, user.ActiveGuild.Id.ToString()),
                    new Claim(AuthorizationConstants.Claims.GuildName, user.ActiveGuild.Name),
                    new Claim(AuthorizationConstants.Claims.GuildRole, role)
                });
            }
            var symmetricKey = Convert.FromBase64String(_config.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = now.AddMinutes(Convert.ToInt32(_config.TtlMinutes)),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey), 
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = _config.Audience,
                Issuer = _config.Authority
            };
            var sToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(sToken);

            return token;
        }
    }
}