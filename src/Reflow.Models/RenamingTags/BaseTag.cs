using Reflow.Contract.DTO;
using Reflow.Contract.Entity;
using System.Collections.Generic;

namespace Reflow.Models.RenamingTags
{
    public abstract class BaseTag : ITag
    {
        protected BaseTag(string name)
        {
            this.TagType = name;
        }

        public string TagType { get; }
        public abstract string Render(string fileName, IDictionary<string, ReflowFile> files);
    }
}
