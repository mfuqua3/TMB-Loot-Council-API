using System.Security.Claims;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class ApiController : Controller
    {
        protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        protected int GuildId => int.Parse(User.FindFirstValue(AuthorizationConstants.Claims.GuildId));
    }
}