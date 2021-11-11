using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    public class GuildsController : ApiController
    {
        private readonly IUserDataService _userDataService;

        public GuildsController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GuildResponse>>> GetUserGuilds()
        {
            var response = await _userDataService.GetUserGuilds(UserId);
            return Ok(response);
        }
        [HttpGet("{id}/configure")]
        public async Task ClaimGuild()
        {
            
        }
    }
}