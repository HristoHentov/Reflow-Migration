using System;
using System.Collections.Generic;
using System.Text;

namespace Reflow.Models.Internal
{
    public class ReflowOption
    {
        public ReflowOption()
        {

        }

        public ReflowOption(string name, string type, int defaultValue, string[] values)
        {
            Name = name;
            Type = type;
            DefaultValue = defaultValue;
            Values = values;
        }
        public string Name { get; set; }

        public string Type { get; set; }

        public int DefaultValue { get; set; }

        public string[] Values { get; set; }
    }
}
