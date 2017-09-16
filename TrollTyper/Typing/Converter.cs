using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static TrollTyper.Utilities;

namespace TrollTyper
{
    class Converter
    {
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

        public bool ConvertText(ref string text, bool isBbcMode)
        {
            string[] lines = Regex.Split(text, nextLines);
            StringBuilder sb = new StringBuilder();

            if (isBbcMode)
            {
                sb.Append("[spoiler open=\"Open Pesterlog\" close=\"Close Pesterlog\"][left]");
            }

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] splitString = line.Split(seperator, 2);


                if (TypingQuirks.TryGetValue(splitString[0], out TypingQuirk quirk))
                {
                    if (isBbcMode)
                    {
                        Color c = quirk.chatColor;
                        sb.Append(string.Format("[color=#{0}]", Utilities.ColorToHex(c)));
                    }

                    sb.Append(quirk.chatHandle);
                    sb.Append(seperator[0]);
                    sb.Append(quirk.ApplyQuirk(splitString[1], isBbcMode));
                    if (isBbcMode)
                    {
                        sb.Append("[/color]");
                    }

                    if (i + 1 < lines.Length)
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


            if (isBbcMode)
            {
                sb.Append("[/left][/spoiler]");
            }

            text = sb.ToString();
            return true;
        }
    }
}
