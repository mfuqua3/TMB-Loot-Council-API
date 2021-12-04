using System.Threading.Tasks;

namespace LootCouncil.Utility.Wowhead
{
    public interface IWowheadClient
    {
        Task<WowheadDataObject> Get(string item);
    }
}