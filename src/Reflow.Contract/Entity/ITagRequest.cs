using System.Collections.Generic;

namespace Reflow.Contract.Entity
{
    public interface ITagRequest
    {
        int OrderId { get; set; }

        string TagType { get; set; }

        dynamic Options { get; set; }
    }
}
