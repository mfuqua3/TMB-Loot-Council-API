using LootCouncil.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LootCouncil.Domain.Data
{
    public class LootCouncilDbContext : IdentityDbContext<LootCouncilUser>
    {
        public LootCouncilDbContext(DbContextOptions options): base(options)
        {
            
        }
    }
}