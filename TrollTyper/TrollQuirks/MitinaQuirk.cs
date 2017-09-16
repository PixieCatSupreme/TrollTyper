using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.TrollQuirks
{
    public class MitinaQuirk : TypingQuirk
    {
        public MitinaQuirk() : base()
        {
            chatHandle = "FF";
            chatColor = System.Drawing.Color.FromArgb(161, 0, 0);

            replacements.Add(new ValueReplacement("W", "oWo", true));
            replacements.Add(new ValueReplacement("l", "w"));
            replacements.Add(new ValueReplacement("L", "W"));
            replacements.Add(new ValueReplacement("a", "aw"));
            replacements.Add(new ValueReplacement("A", "Aw"));
            replacements.Add(new ValueReplacement("r", "w"));
            replacements.Add(new ValueReplacement("R", "W"));
            replacements.Add(new ValueReplacement("p", "pw"));
            replacements.Add(new ValueReplacement("P", "Pw"));
            replacements.Add(new ValueReplacement("you", "U", true));
        }
    }
}
