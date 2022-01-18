using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.DataContracts.Identity.Model;
using LootCouncil.Service.Core;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    public class GuildsController : ApiController
    {
        private readonly IUserDataService _userDataService;
        private readonly IGuildService _guildService;

        public GuildsController(IUserDataService userDataService, IGuildService guildService)
        {
            _userDataService = userDataService;
            _guildService = guildService;
        }
        [HttpGet("users")]
        [GuildScoped]
        public async Task<ActionResult<List<GuildUserResponse>>> GetGuildUsers()
        {
            var response = await _guildService.GetGuildUsers(GuildId);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<List<DiscordServerResponse>>> GetUserServers()
        {
            var response = await _userDataService.GetUserServers(UserId);
            return Ok(response);
        }
        [HttpPost("configure")]
        public async Task<ActionResult<GuildSummaryResponse>> ClaimGuild(ClaimDiscordServerRequest request)
        {
            request.UserId = UserId;
            var response = await _guildService.ClaimDiscordServer(request);
            return Created("", response);
        }
        [HttpDelete("release/{guildId}")]
        [Authorize(Roles = "Admin,Developer")]
        public async Task<IActionResult> ReleaseGuild(int guildId)
        {
            await _guildService.ReleaseGuild(UserId, guildId);
            return NoContent();
        }
        [HttpGet("select/{id}")]
        public async Task<ActionResult<Token>> ChangeGuildScope(int id)
        {
            var token = await _guildService.ChangeGuildScope(UserId, id);
            return Ok(new Token() { AccessToken = token });
        }
    }
}