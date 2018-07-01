using System;
using System.Collections.Generic;
using Reflow.Contract.Entity;
using Reflow.Contract.DTO;

namespace Reflow.Contract.DataExchange
{
    public interface IDataImporter
    {
        ITag ParseTag(string type, string json, IDictionary<string, Type> avaiableTypes);

        IList<ITag> ParseTagCollection(string json, IDictionary<string, Type> avaiableTypes);

        IDictionary<string, ReflowFile> ParseFiles(string filesJson);

        IRenameOptionsSet ParseSettings(string settingsJson);
    }
}
