using LootCouncil.Utility.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
    [AllowAnonymous]
    public class CoffeeController: ApiController
    {
        [HttpGet]
        public IActionResult BrewCoffee() => throw new ServerIsTeapotException();
    }
}