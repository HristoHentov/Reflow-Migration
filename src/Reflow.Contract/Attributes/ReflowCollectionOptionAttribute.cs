using System;

namespace Reflow.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ReflowCollectionOptionAttribute : ReflowOptionAttribute
    {
        public string[] EnabledValues { get; set; } = new string[] { };

        public string[] DisabledValues { get; set; } = new string[] { };
    }
}
