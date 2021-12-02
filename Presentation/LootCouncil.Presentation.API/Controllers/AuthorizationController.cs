using System.Threading.Tasks;
using AspNet.Security.OAuth.Discord;
using LootCouncil.Service.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    [AllowAnonymous]
    public class AuthorizationController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthorizationController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("~/connect/discord")]
        public IActionResult Discord(string returnUrl)
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("HandleDiscordLogin", new {returnUrl})
            };
            return Challenge(authenticationProperties, DiscordAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpGet("~/signin-external")]
        public async Task<IActionResult> HandleDiscordLogin(string returnUrl)
        {
            var claimsPrincipal = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (!claimsPrincipal.Succeeded)
            {
                return Unauthorized();
            }

            var discordAccessToken = claimsPrincipal.Properties.Items[".Token.access_token"];
            var tokenResponse = await _accountService.DiscordAuthorize(discordAccessToken);
            returnUrl = returnUrl.Replace(":token", tokenResponse.AccessToken);
            return Redirect(returnUrl);
        }
    }
}