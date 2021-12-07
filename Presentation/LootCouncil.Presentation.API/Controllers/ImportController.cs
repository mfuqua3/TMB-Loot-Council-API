using System.Collections.Generic;
using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;
using LootCouncil.Domain.DataContracts.ThatsMyBis;
using LootCouncil.Service.Core;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    public class ImportController : ApiController
    {
        private readonly IImportService _importService;

        public ImportController(IImportService importService)
        {
            _importService = importService;
        }
        [HttpPost]
        [GuildScoped(AuthorizationConstants.GuildRoles.Owner)]
        public async Task<ActionResult<ImportResponse>> ImportTmbRoster(List<TmbCharacter> tmbCharacters)
        {
            var import = await _importService.CreateImportAsync(new CreateImportRequest
            {
                GuildId = GuildId,
                UserId = UserId,
                Data = new TmbRosterState
                {
                    Characters = tmbCharacters
                }
            });
            return Created("", import);
        }

        [HttpGet("{id}")]
        [GuildScoped]
        public async Task<ActionResult<ImportResponse>> GetImportStatus(int id)
        {
            return Ok(await _importService.GetImportAsync(id, GuildId));
        }
    }
}