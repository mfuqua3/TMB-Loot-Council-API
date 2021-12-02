using System.Text.Json.Serialization;

namespace LootCouncil.Utility.Converters
{
    public class JsonNumberToBooleanAttribute : JsonConverterAttribute
    {
        public JsonNumberToBooleanAttribute():base(typeof(NumberToBooleanConverter))
        {
            
        }
    }
}