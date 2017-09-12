using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrollTyper
{
    public class TypingQuirk
    {
        public string chatHandle;
        protected List<KeyValuePair<string, string>> replacements;

        public TypingQuirk()
        {
            chatHandle = "NULL";
            replacements = new List<KeyValuePair<string, string>>();
        }

        public string ApplyQuirk(string text)
        {
            for (int i = 0; i < replacements.Count; i++)
            {
                KeyValuePair<string, string> oldNewValue = replacements[i];
                text = Regex.Replace(text, oldNewValue.Key, oldNewValue.Value, RegexOptions.IgnoreCase);
            }
            return text;
        }
    }
}
