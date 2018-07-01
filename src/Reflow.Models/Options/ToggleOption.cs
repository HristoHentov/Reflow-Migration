using Reflow.Contract.Enum;
using Reflow.Models.Options.Base;

namespace Reflow.Models.Options
{
    public class ToggleOption : SingleOption
    {
        public ToggleOption(int id, string name, bool def)
        {
            Id = id;
            Name = name;
            Default = def;
        }
        public override int Id { get; }
        public override string Name { get; }
        public override OptionType Type => OptionType.Toggle;
        public override object Default { get; }
    }
}
