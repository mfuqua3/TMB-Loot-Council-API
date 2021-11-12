using System;
using System.Text.Json.Serialization;

namespace LootCouncil.Domain.DataContracts.ThatsMyBis
{
    public class TmbCharacterItemDetails
    {
        public int CharacterId { get; set; }
        public int ItemId { get; set; }
        public int Id { get; set; }
        [JsonPropertyName("added_by")]
        public int AddedBy { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
        [JsonPropertyName("list_number")]
        public int ListNumber { get; set; }
        [JsonPropertyName("is_offspec")]
        public bool IsOffspec { get; set; }
        [JsonPropertyName("is_received")]
        public bool IsReceived { get; set; }
        [JsonPropertyName("received_at")]
        public DateTime? ReceivedAt { get; set; }
        public string Note { get; set; }
        [JsonPropertyName("raid_group_id")]
        public int? RaidGroupId { get; set; }
        [JsonPropertyName("raid_id")]
        public int? RaidId { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}