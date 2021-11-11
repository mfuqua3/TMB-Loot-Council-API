using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LootCouncil.Engine
{
    public class JwtEngine : IJwtEngine
    {
        private readonly UserManager<LootCouncilUser> _userManager;
        private readonly JwtTokenOptions _config;

        public JwtEngine(
            IOptions<JwtTokenOptions> options, 
            UserManager<LootCouncilUser> userManager)
        {
            _userManager = userManager;
            _config = options.Value;
        }
        public async Task<string> GenerateToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            });
            claims.AddClaims(roles.Select(x => new Claim(ClaimTypes.Role, x)));
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