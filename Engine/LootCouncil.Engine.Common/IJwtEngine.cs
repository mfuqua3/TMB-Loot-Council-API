using System.Threading.Tasks;
using LootCouncil.Domain.Entities;

namespace LootCouncil.Engine
{
    public interface IJwtEngine
    {
        Task<string> GenerateToken(string userId);
    }
}