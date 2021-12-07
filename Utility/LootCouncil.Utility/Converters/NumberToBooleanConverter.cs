using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LootCouncil.Utility.Converters
{
    public class TmbNullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (dateString == null) return null;
            return DateTime.TryParse(dateString, out var date) ? date : null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
    public class NumberToBooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Number:
                    return reader.TryGetByte(out var byteVal) && byteVal == 1;
                case JsonTokenType.String:
                    return reader.GetString()?.Equals("1") ?? false;
                case JsonTokenType.Null:
                    return false;
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            var number = value ? 1 : 0;
            writer.WriteNumberValue(number);
        }
    }
}