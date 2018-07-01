using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reflow.Contract.DataExchange;
using Reflow.Contract.Entity;
using Reflow.Models.Internal;
using Reflow.Contract.DTO;

namespace Reflow.Core.DataExchnage
{
    internal class JsonDataImporter : IDataImporter
    {
        public ITag ParseTag(string type, string json, IDictionary<string, Type> avaiableTypes)
        {
            return (ITag)JsonConvert.DeserializeObject(json, avaiableTypes[type]);
        }

        public IList<ITag> ParseTagCollection(string json, IDictionary<string, Type> avaiableTypes)
        {
            var typesArray = JArray.Parse(json);
            return typesArray
                .Select(t => ParseTagEntry(t, avaiableTypes[t[nameof(ITag.TagType)].ToString()]))
                .ToList();
        }

        public IDictionary<string, ReflowFile> ParseFiles(string filesJson)
        {
            var files = JArray.Parse(filesJson);

            return files
                .Select(ParseFileToken)
                .ToDictionary(e => e.OriginalName);
        }

        public IRenameOptionsSet ParseSettings(string settingsJson)
        {
            return JsonConvert.DeserializeObject<ReflowRenameOptionSet>(settingsJson);
        }

        private ReflowFile ParseFileToken(JToken jToken)
        {
            return JsonConvert.DeserializeObject<ReflowFile>(jToken.ToString());
        }


        private ITag ParseTagEntry(JToken token, Type type)
        {
            return (ITag)JsonConvert.DeserializeObject(token["Options"].ToString(), type);
        }


    }
}
