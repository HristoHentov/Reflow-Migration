using System.Collections.Generic;
using Reflow.Contract.DTO;

namespace Reflow.Models.RenamingTags
{
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
