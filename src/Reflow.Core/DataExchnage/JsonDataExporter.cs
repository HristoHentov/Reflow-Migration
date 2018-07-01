using Newtonsoft.Json;
using Reflow.Contract.DataExchange;
using Reflow.Models.Internal;

namespace Reflow.Core.DataExchnage
{
    internal class JsonDataExporter : IDataExporter<string>
    {
        private bool _idented = true;

        public string Export(object data)
        {
            return JsonConvert.SerializeObject(data, _idented ? Formatting.Indented : Formatting.None);
        }

        public string ExportSuccess(object data)
        {
            return JsonConvert.SerializeObject(new APIResponse { Response = data }, _idented ? Formatting.Indented : Formatting.None);
        }

        public string ExportError(object data)
        {
            return JsonConvert.SerializeObject(new APIResponse { Error = data }, _idented ? Formatting.Indented : Formatting.None);
        }
    }
}
