using System;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Model;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Service.Core;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    [GuildScoped]
    public class PreVoteController : ApiController
    {
        private readonly IPreVoteService _preVoteService;

        public PreVoteController(IPreVoteService preVoteService)
        {
            _preVoteService = preVoteService;
        }

        [GuildScoped(AuthorizationConstants.GuildRoles.Owner, AuthorizationConstants.GuildRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult<PreVoteSummary>> CreatePreVote(CreatePreVoteRequest request)
        {
            request.GuildId = GuildId;
            var response = await _preVoteService.CreatePreVote(request);
            return Created("", response);
        }

        [GuildScoped(AuthorizationConstants.GuildRoles.Owner, AuthorizationConstants.GuildRoles.Admin)]
        [HttpGet("configuration/default")]
        public async Task<ActionResult<PreVoteConfigurationModel>> GetDefaultConfiguration()
            => Ok(await _preVoteService.GetConfiguration(1));

        [GuildScoped(AuthorizationConstants.GuildRoles.Owner, AuthorizationConstants.GuildRoles.Admin)]
        [HttpGet("configuration/latest")]
        public async Task<ActionResult<PreVoteConfigurationModel>> GetLatestConfiguration()
            => Ok(await _preVoteService.GetLatestConfiguration(GuildId));
    }
}