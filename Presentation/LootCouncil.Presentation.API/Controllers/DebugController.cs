using System.Linq;
using System.Threading.Tasks;
using LootCouncil.Domain.Data;
using LootCouncil.Engine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LootCouncil.Presentation.API.Controllers
{
#if DEBUG
    public class DebugController : ApiController
    {
        private readonly LootCouncilDbContext _db;
        private readonly IJwtEngine _jwtEngine;

        public DebugController(LootCouncilDbContext db, IJwtEngine jwtEngine)
        {
            _db = db;
            _jwtEngine = jwtEngine;
        }
        [HttpGet("token")]
        [AllowAnonymous]
        public async Task<IActionResult> Token()
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == "fuqua.matt@gmail.com");
            var token = await _jwtEngine.GenerateToken(user.Id);
            return Ok(token);
        }
    }
#endif
}