using Reflow.Contract.DTO;
using System.Collections.Generic;

namespace Reflow.Contract.Entity
{
    public interface ITag
    {
        string TagType { get; }

        string Render(string fileName, IDictionary<string, ReflowFile> files);
    }
}
 