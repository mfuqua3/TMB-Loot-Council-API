using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LootCouncil.Utility.Authorization
{
    public static class AuthorizationFilterContextExtensions
    {
        public static void Forbid(this AuthorizationFilterContext _, string message)
        {
            throw new UnauthorizedAccessException(message);
        }
    }
}