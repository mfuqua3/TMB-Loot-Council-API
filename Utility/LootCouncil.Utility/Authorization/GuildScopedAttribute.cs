using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http.Abstractions;

namespace LootCouncil.Utility.Authorization
{
    public class GuildScopedAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string GuildRoles { get; }

        public GuildScopedAttribute()
        {}

        public GuildScopedAttribute(params string[] roles)
        {
            GuildRoles = string.Join(',', roles);
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roles = GuildRoles?.Split(',')?.ToArray() ?? new string[0];
            var guildClaim = context.HttpContext.User.FindFirst(AuthorizationConstants.Claims.GuildId);
            if (guildClaim == null)
            {
                throw new InvalidOperationException(
                    "Invalid user state for this request. User token has not been scoped to a guild");
            }
            if (roles.Length == 0)
            {
                return;
            }
            var guildRole = context.HttpContext.User.FindFirst(AuthorizationConstants.Claims.GuildRole)?.Value;
            if (guildRole != null && roles.Contains(guildRole)) return;
            context.Forbid(
                "Unauthorized. You do not have sufficient permission to take this action for your current guild.");
        }
    }
}