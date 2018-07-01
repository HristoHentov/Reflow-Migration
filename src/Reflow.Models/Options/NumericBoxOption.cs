using Reflow.Contract.Enum;
using Reflow.Models.Options.Base;

namespace Reflow.Models.Options
{
    public class NumericBoxOption : SingleOption
    {
        public NumericBoxOption(int id, string name, int def)
        {
            Id = id;
            Name = name;
            Default = def;
        }

        public override int Id { get; }
        public override string Name { get; }
        public override OptionType Type => OptionType.NumericBox;
        public override object Default { get; }
    }
}
