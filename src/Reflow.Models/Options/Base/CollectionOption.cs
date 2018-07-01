using System.Collections.Generic;
using Reflow.Contract.Entity;
using Reflow.Contract.Enum;

namespace Reflow.Models.Options.Base
{
    public abstract class CollectionOption : ITagOption
    {
        public abstract int Id { get; }

        public abstract string Name { get; }

        public abstract OptionType Type { get; }

        public abstract object Default { get; }

        public abstract ICollection<CollectionItem> Items { get; }
    }
}
