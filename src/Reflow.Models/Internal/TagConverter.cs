using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Reflow.Models.Internal
{
    public class TagConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null)
                return string.Empty;
            if (reader.TokenType == JsonToken.StartObject)
                return serializer.Deserialize(reader, objectType);

            JObject obj = JObject.Load(reader);
            if (obj["Options"] != null)
                return obj["Options"].ToString();
            else
                return serializer.Deserialize(reader, objectType);

        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
