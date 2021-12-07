using System.Threading.Tasks;

namespace LootCouncil.Engine
{
    public interface IJwtEngine
    {
        Task<string> GenerateToken(string userId);
    }
}