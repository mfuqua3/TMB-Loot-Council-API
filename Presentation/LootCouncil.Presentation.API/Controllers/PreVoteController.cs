using System;
using System.Threading.Tasks;
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
        public async Task<ActionResult<PreVoteResponse>> CreatePreVote(CreatePreVoteRequest request)
        {
            request.GuildId = GuildId;
            var response = await _preVoteService.CreatePreVote(request);
            return Created("", response);
        }
    }
}