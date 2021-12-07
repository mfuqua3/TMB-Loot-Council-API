using System.Collections.Generic;
using System.Text.Json.Serialization;
using LootCouncil.Utility.Converters;

namespace LootCouncil.Domain.DataContracts.ThatsMyBis
{
    public class TmbCharacter
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int GuildId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Level { get; set; }
        public string Race { get; set; }
        public string Class { get; set; }
        public string Spec { get; set; }
        public string ArcheType { get; set; }
        [JsonPropertyName("spec_label")]
        public string SpecLabel { get; set; }
        [JsonPropertyName("profession_1")]
        public string Profession1 { get; set; }
        [JsonPropertyName("profession_2")]
        public string Profession2 { get; set; }
        public string Rank { get; set; }
        [JsonPropertyName("rank_goal")]
        public string RankGoal { get; set; }
        [JsonPropertyName("raid_group_id")]
        public int? RaidGroupId { get; set; }
        [JsonPropertyName("is_alt")]
        [JsonNumberToBoolean]
        public bool IsAlt { get; set; }
        public string Username { get; set; }
        [JsonPropertyName("member_slug")]
        public string MemberSlug { get; set; }
        [JsonPropertyName("discord_id")]
        public ulong? DiscordId { get; set; }
        [JsonPropertyName("discord_username")]
        public string DiscordUsername { get; set; }
        [JsonPropertyName("is_wishlist_unlocked")]
        [JsonNumberToBoolean]
        public bool IsWishlistUnlocked { get; set; }
        [JsonPropertyName("is_received_unlocked")]
        [JsonNumberToBoolean]
        public bool IsReceivedUnlocked { get; set; }
        [JsonPropertyName("raid_group_name")]
        public string RaidGroupName { get; set; }
        [JsonPropertyName("raid_group_color")]
        public string RaidGroupColor { get; set; }
        [JsonPropertyName("officer_note")]
        public string OfficerNote { get; set; }
        [JsonPropertyName("raid_count")]
        public int RaidCount { get; set; }
        [JsonPropertyName("benched_count")]
        public int BenchedCount { get; set; }
        [JsonPropertyName("attendance_percentage")]
        public double AttendancePercentage { get; set; }
        [JsonPropertyName("display_archetype")]
        public string DisplayArchetype { get; set; }
        [JsonPropertyName("display_class")]
        public string DisplayClass { get; set; }
        [JsonPropertyName("display_profession1")]
        public string DisplayProfession1 { get; set; }
        [JsonPropertyName("display_profession2")]
        public string DisplayProfession2 { get; set; }
        [JsonPropertyName("display_race")]
        public string DisplayRace { get; set; }
        [JsonPropertyName("display_spec")]
        public string DisplaySpec { get; set; }
        [JsonPropertyName("sub_archetype")]
        public string SubArchetype { get; set; }
        public List<TmbItem> Received { get; set; }
        public List<TmbItem> Prios { get; set; }
        public List<TmbItem> Wishlist { get; set; }
    }
}