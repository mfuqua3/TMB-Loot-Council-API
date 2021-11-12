using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LootCouncil.Domain.Entities;
using LootCouncil.Utility.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LootCouncil.Engine
{
    internal class JwtEngine : IJwtEngine
    {
        private readonly JwtTokenOptions _config;

        public JwtEngine(IOptions<JwtTokenOptions> options)
        {
            _config = options.Value;
        }
        public string GenerateToken(LootCouncilUser user)
        {
            var symmetricKey = Convert.FromBase64String(_config.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
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