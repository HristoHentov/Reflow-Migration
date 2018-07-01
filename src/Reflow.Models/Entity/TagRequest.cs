using System.Collections.Generic;
using Reflow.Contract.Entity;

namespace Reflow.Models.Entity
{
    public class TagRequest : ITagRequest
    {
        public int OrderId { get; set; }

        public string TagType { get; set; }

        public dynamic Options { get; set; }
    }
}
