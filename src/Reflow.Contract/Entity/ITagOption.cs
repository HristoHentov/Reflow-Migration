using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Reflow.Contract.Enum;

namespace Reflow.Contract.Entity
{
    public interface ITagOption
    {
        int Id { get; }

        string Name { get; }

        [JsonConverter(typeof(StringEnumConverter))]
        OptionType Type { get;}

        [JsonProperty("Value")]
        object Default { get;}
    }
}
