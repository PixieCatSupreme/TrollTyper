using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.TrollQuirks
{
    class KadopiQuirk : TypingQuirk
    {
        public KadopiQuirk() : base("couchPotato")
        {
            ChatColor = System.Drawing.Color.FromArgb(0, 130, 130);

            replacements.Add(new ValueReplacement("m", "mmmm"));
            replacements.Add(new ValueReplacement("oo", "o"));
            replacements.Add(new ValueReplacement("ee", "e"));
            replacements.Add(new ValueReplacement("aa", "a"));
            replacements.Add(new ValueReplacement("ie", "y"));
            replacements.Add(new ValueReplacement("ei", "y"));
            replacements.Add(new ValueReplacement("why", "y?"));
            replacements.Add(new ValueReplacement("who", "o?"));
            replacements.Add(new ValueReplacement("where", "wer?"));
            replacements.Add(new ValueReplacement("us", "s"));
            replacements.Add(new ValueReplacement("es", "s"));
            replacements.Add(new ValueReplacement("as", "s"));
        }

        public override string ApplyQuirk(string text, bool isBbcMode)
        {
            text = text.ToLower();
            int spaceNumber = 0;
            for (int i = text.Length-1; i >= 0; i--)
            {
                if (text[i] == ' ')
                {
                    spaceNumber++;
                    if (spaceNumber == 3)
                    {
                        spaceNumber = 0;
                        text = text.Remove(i, 1);
                    }
                }
            }

            return base.ApplyQuirk(text, isBbcMode);
        }
    }
}
