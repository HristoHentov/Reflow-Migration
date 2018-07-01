using System;
using System.Collections.Generic;
using Reflow.Contract.Entity;
using Reflow.Contract.DTO;

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

            foreach (var file in files)
            {
                string newName = string.Empty;
                foreach (var tag in Tags)
                    newName += (tag.Render(file.Key, files));


                file.Value.NewName = newName;
                res.Add(file.Value);
                
            }
            return res;
        }

        public void Flush()
        {
            this.Tags.Clear();
        }
    }
}
