using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.Core.Request;
using LootCouncil.Domain.DataContracts.Core.Response;

namespace LootCouncil.Service.Core
{
    public interface IImportService
    {
        Task<ImportResponse> CreateImportAsync(CreateImportRequest request);
        Task<ImportResponse> GetImportAsync(int importId, int guildId);
    }
}