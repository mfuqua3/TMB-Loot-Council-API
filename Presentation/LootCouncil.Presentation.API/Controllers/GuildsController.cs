using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Service.Core;
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

        [HttpGet]
        public async Task<ActionResult<List<GuildResponse>>> GetUserGuilds()
        {
            var response = await _userDataService.GetUserGuilds(UserId);
            return Ok(response);
        }
        [HttpPost("configure")]
        public async Task<ActionResult<ClaimGuildResponse>> ClaimGuild(ClaimGuildRequest request)
        {
            request.UserId = UserId;
            var response = await _guildService.ClaimGuild(request);
            return Created("", response);
        }
    }
}