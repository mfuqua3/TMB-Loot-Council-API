using System.Collections.Generic;
using LootCouncil.Domain.DataContracts.ThatsMyBis;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    
    public class ImportController : ApiController
    {
        [HttpPost]
        public IActionResult ImportTmbRoster(List<TmbCharacter> tmbCharacters)
        {
            return Created("", null);
        }
    }
}