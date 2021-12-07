using System.Threading.Tasks;
using LootCouncil.Domain.DataContracts.ThatsMyBis;

namespace LootCouncil.Engine
{
    public interface IThatsMyBisDataEngine
    {
        Task ImportData(int importId, TmbRosterState tmbRosterState);
    }
}