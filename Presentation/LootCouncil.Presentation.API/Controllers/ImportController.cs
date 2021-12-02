using System.Collections.Generic;
using LootCouncil.Domain.DataContracts.ThatsMyBis;
using LootCouncil.Utility.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    public class ImportController : ApiController
    {
        [HttpPost]
        [GuildScoped(AuthorizationConstants.GuildRoles.Owner)]
        public IActionResult ImportTmbRoster(List<TmbCharacter> tmbCharacters)
        {
            return Created("", null);
        }
    }
}