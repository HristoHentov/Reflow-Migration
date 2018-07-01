using Reflow.Contract.Enum;
using Reflow.Models.Options.Base;

namespace Reflow.Models.Options
{
    public class CheckBoxOption : SingleOption
    {
        public CheckBoxOption(int id, string name, bool def)
        {
            this.Id = id;
            this.Name = name;
            this.Default = def;
        }
        public override int Id { get; }

        public override string Name { get; }

        public override OptionType Type => OptionType.CheckBox;

        public override object Default { get; }
    }
}
