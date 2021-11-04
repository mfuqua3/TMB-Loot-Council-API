using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        [HttpGet("~/connect/discord")]
        public IActionResult Discord()
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("HandleExternalLogin")
            };
            return Challenge(authenticationProperties, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("~/signin-external")]
        public async Task<IActionResult> HandleExternalLogin()
        {
            var claimsPrincipal = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            return Ok();
        }
    }
}