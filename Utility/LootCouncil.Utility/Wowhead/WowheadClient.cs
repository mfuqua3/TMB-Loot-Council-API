using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LootCouncil.Utility.Wowhead
{
    public class WowheadClient : IWowheadClient
    {
        private readonly HttpClient _client;

        public WowheadClient()
        {
            _client = new HttpClient {BaseAddress = new Uri(@"https://tbc.wowhead.com/")};
        }

        public async Task<WowheadDataObject> Get(string item)
        {
            var httpResult = await _client.GetAsync($"{_client.BaseAddress}item={item.ToLowerInvariant()}&xml");
            if (!httpResult.IsSuccessStatusCode)
            {
                return null;
            }
            var xmlStream = await httpResult.Content.ReadAsStreamAsync();
            var serializer = new XmlSerializer(typeof(WowheadDataObject));
            var obj = serializer.Deserialize(xmlStream);
            return obj as WowheadDataObject;
        }
    }
}