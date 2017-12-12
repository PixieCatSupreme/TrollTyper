using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static TrollTyper.Utilities;

namespace TrollTyper.TrollQuirks
{
    class NeswelQuirk : TypingQuirk
    {
        public NeswelQuirk() : base("demonSlayer")
        {
            ChatColor = System.Drawing.Color.FromArgb(0, 33, 203);

            replacements.Add(new ValueReplacement("d", "D"));
            replacements.Add(new ValueReplacement("o", "O"));
            replacements.Add(new ValueReplacement("m", "M"));
            replacements.Add(new ValueReplacement("b", "d"));
            replacements.Add(new ValueReplacement("p", "q"));
            replacements.Add(new ValueReplacement("DOOM", 
                String.Format("[/color][b][color=#{0}]DOOM[/color][/b][color=#{1}]", 
                ColorToHex(Color.Black),
                ColorToHex(ChatColor)), 
                false, true));
        }

        public override string ApplyQuirk(string text, bool isBbcMode)
        {
            string[] sentences = Regex.Split(text, @"(?<=[.,;!?])");
            text = "";

            for (int i = 0; i < sentences.Length; i++)
            {
                string sentence = sentences[i];
                if (!string.IsNullOrWhiteSpace(sentence))
                {
                    int words = CountWords(sentence);

                    text += sentence + new string(sentence.Last(), words - 1);
                }
            }

            return base.ApplyQuirk(text, isBbcMode);
        }
    }
}
