using System.Collections.Generic;
using Reflow.Contract.Entity;

namespace Reflow.Contract.DTO
{
    public class TagViewModel : ITagModel
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string TagType { get; set; }

        public string Name { get; set; }

        public ICollection<ITagOption> Options { get; set; }
    }
}
