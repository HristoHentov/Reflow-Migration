using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Reflow.Contract.DTO
{
    [DebuggerDisplay("[O: {OriginalName} T: {Type}] -> N: {NewName}")]
    public class ReflowFile : IComparable<ReflowFile>, IEquatable<ReflowFile>
    {
        public ReflowFile(int id, string originalName, string type, string size, bool selected)
        {
            this.Id = id;
            this.OriginalName = originalName;
            this.NewName = originalName;
            this.Type = type;
            this.Size = Size.Parse(size);
            this.Selected = selected;
            this.FullName = originalName + type;
        }

        public int Id { get; }

        public string OriginalName { get; }

        public string NewName { get; set; }

        public string Type { get; }

        [JsonConverter(typeof(SizeConverter))]
        public Size Size { get; }

        public bool Selected { get; set; }

        public string FullName { get; }

        public int CompareTo(ReflowFile other)
        {
            return this.FullName.CompareTo(other.FullName);
        }

        public bool Equals(ReflowFile other)
        {
            return this.FullName.Equals(other.FullName);
        }
    }

    public class SizeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(String);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var size = new Size(0, Enum.Magnitude.B);

            if(reader.TokenType != JsonToken.Null && reader.TokenType == JsonToken.String)
            {
                size = Size.Parse((string)(new JValue(reader.Value)));
            }

            return size;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var size = value as Size;
            var result = $"{size.Amount} {size.Magnitude}";
            writer.WriteValue(result);
        }
    }

}
