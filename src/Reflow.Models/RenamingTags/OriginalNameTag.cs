using System.Collections.Generic;
using Reflow.Contract.DTO;
using Reflow.Contract.Attributes;

namespace Reflow.Models.RenamingTags
{
    [ReflowTag(Name = "Original Name")]
    public class OriginalNameTag : BaseTag
    {
        protected OriginalNameTag() : base(nameof(OriginalNameTag))
        {
        }

        public override string Render(string fileName, IDictionary<string, ReflowFile> files)
        {
            return fileName;
        }
    }
}
