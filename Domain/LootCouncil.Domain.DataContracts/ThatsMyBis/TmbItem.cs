using System.Text.Json.Serialization;

namespace LootCouncil.Domain.DataContracts.ThatsMyBis
{
    public class TmbItem
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int? ParentId { get; set; }
        public int? ParentItemId { get; set; }
        public int ExpansionId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int Quality { get; set; }
        [JsonPropertyName("inventory_type")]
        public int InventoryType { get; set; }
        [JsonPropertyName("item_source_id")]
        public int ItemSourceId { get; set; }
        [JsonPropertyName("instance_id")]
        public int InstanceId { get; set; }
        [JsonPropertyName("instance_name")]
        public string InstanceName { get; set; }
        [JsonPropertyName("instance_order")]
        public int InstanceOrder { get; set; }
        [JsonPropertyName("added_by_username")]
        public string AddedByUsername { get; set; }
        [JsonPropertyName("guild_tier")]
        public string GuildTier { get; set; }
        [JsonPropertyName("list_number")]
        public int ListNumber { get; set; }
        [JsonPropertyName("pivot")]
        public TmbCharacterItemDetails Details { get; set; }
    }
}