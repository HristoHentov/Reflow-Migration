using System;
using System.Collections.Generic;
using Reflow.Contract.Entity;
using Reflow.Contract.DTO;
using System.Text;

namespace Reflow.Models.Entity.Tag
{
    public class NameBuilder
    {
        private static readonly Lazy<NameBuilder> _singletonInstance = new Lazy<NameBuilder>(() => new NameBuilder());

        public static NameBuilder Instance => _singletonInstance.Value;


        private NameBuilder()
        {
            Tags = new List<ITag>();
        }

        public IList<ITag> Tags { get; set; }

        public IList<ReflowFile> Resolve(IDictionary<string, ReflowFile> files)
        {
            var res = new List<ReflowFile>();
            var sb = new StringBuilder();
            foreach (var file in files)
            {
                foreach (var tag in Tags)
                {
                    sb.Append((tag.Render(file.Value.OriginalName, files)));
                }


                file.Value.NewName = sb.ToString();
                res.Add(file.Value);
                sb.Clear();
            }
            return res;
        }

        public void Flush()
        {
            this.Tags.Clear();
        }
    }
}
