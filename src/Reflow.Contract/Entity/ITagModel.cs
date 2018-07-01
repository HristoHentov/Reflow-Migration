using System.Collections.Generic;

namespace Reflow.Contract.Entity
{
    public interface ITagModel
    {
        int Id { get; set; }

        int OrderId { get; set; }

        string TagType { get; }

        string Name { get; set; }

        ICollection<ITagOption> Options { get; set; }
    }
}
