using Reflow.Contract.Enum;
using Reflow.Models.Options.Base;

namespace Reflow.Models.Options
{
    public class TextAreaOption : SingleOption
    {
        public TextAreaOption(int id, string name, string def)
        {
            Id = id;
            Name = name;
            Default = def;
        }
        public override int Id { get; }
        public override string Name { get; }
        public override OptionType Type => OptionType.TextArea;
        public override object Default { get; }
    }
}
