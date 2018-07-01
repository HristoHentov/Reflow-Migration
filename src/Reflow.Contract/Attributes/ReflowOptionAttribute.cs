using Reflow.Contract.Entity;
using System;

namespace Reflow.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ReflowOptionAttribute : Attribute
    {
        public Type OptionType { get; set; }

        public string Name { get; set; }

        public object Default { get; set; }
    }
}
