using System.Collections.Generic;
using Reflow.Contract.Enum;
using Reflow.Models.Options.Base;

namespace Reflow.Models.Options
{
    public class CheckListOption : CollectionOption
    {
        public CheckListOption(int id, string name, string def, List<CollectionItem> values = null)
        {
            this.Id = id;
            this.Default = def;
            this.Name = name;
            this.Items = values ?? new List<CollectionItem>();
        }

        public override int Id { get; }

        public override string Name { get; }

        public override OptionType Type => OptionType.CheckList;

        public override object Default { get; }

        public override ICollection<CollectionItem> Items { get; }
    }
}
