using System.Threading.Tasks;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine
{
    public interface IJwtEngine
    {
        string GenerateToken(LootCouncilUser user);
    }
}