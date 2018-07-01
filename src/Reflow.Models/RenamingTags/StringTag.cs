using System.Collections.Generic;
using Newtonsoft.Json;
using Reflow.Contract.Entity;
using Reflow.Contract.Attributes;
using Reflow.Contract.DTO;

namespace Reflow.Models.RenamingTags
{
    public class StringTag : BaseTag
    {
        public StringTag() : base(nameof(StringTag))
        {
        }

        [JsonConstructor]
        public StringTag(string text) : base(nameof(StringTag))
        {
            this.Text = text;
        }

        [ReflowOption]
        public string Text { get; set; }

        public override string Render(string fileName, IDictionary<string, ReflowFile> files)
        {
            return this.Text;
        }
    }
}
