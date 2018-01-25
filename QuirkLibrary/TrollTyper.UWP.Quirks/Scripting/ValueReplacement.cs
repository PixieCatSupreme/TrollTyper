using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.Quirks.Scripting
{
    public class ValueReplacement
    {
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }
        public bool IgnoreCase { get; private set; }
        public bool HtmlModeOnly { get; private set; }

        public ValueReplacement(string oldValue, string newValue, bool ignoreCase = false, bool htmlModeOnly = false)
        {
            OldValue = oldValue;
            NewValue = newValue;
            IgnoreCase = ignoreCase;
            HtmlModeOnly = htmlModeOnly;
        }
    }
}
