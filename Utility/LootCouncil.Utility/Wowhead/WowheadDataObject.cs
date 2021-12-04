using System.Xml.Serialization;

namespace LootCouncil.Utility.Wowhead
{
    [XmlRoot(ElementName = "wowhead")]
    public class WowheadDataObject
    {
        [XmlElement(ElementName = "item")]
        public XmlWowheadItem Item { get; set; }
    }
}