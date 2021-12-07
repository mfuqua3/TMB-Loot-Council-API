using System.Xml.Serialization;

namespace LootCouncil.Utility.Wowhead
{
    [XmlRoot(ElementName = "createdBy")]
    public class CreatedBy
    {
        [XmlElement(ElementName = "spell")]
        public Spell Spell { get; set; }
    }
}