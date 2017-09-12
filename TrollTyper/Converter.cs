using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrollTyper
{
    class Converter
    {
        private char[] seperator = new char[] { ':' };
        private string nextLines = "\r\n|\r|\n";

        public Dictionary<string, TypingQuirk> TypingQuirks { get; set; }

        public Converter(params TypingQuirk[] quirks)
        {
            TypingQuirks = new Dictionary<string, TypingQuirk>();
            for (int i = 0; i < quirks.Length; i++)
            {
                TypingQuirk q = quirks[i];
                TypingQuirks.Add(q.chatHandle, q);
            }

        }

        public bool ConvertText(ref string text)
        {
            string[] lines = Regex.Split(text, nextLines);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitString = line.Split(seperator, 2);


                if (TypingQuirks.TryGetValue(splitString[0], out TypingQuirk quirk))
                {
                    sb.Append(quirk.chatHandle);
                    sb.Append(seperator[0]);
                    sb.Append(quirk.ApplyQuirk(splitString[1]));
                    if (i+1 < lines.Length)
                    {
                        sb.AppendLine();
                    }
                }
                else
                {
                    Console.WriteLine("No quirk found for character with userHandle: " + splitString[0] + "!");
                    return false;
                }
            }

            text = sb.ToString();
            return true;
        }
    }
}
