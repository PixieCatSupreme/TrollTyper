using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.TrollQuirks
{
    public class MitinaQuirk : TypingQuirk
    {
        public MitinaQuirk() : base("fierceFeline")
        {
            chatColor = System.Drawing.Color.FromArgb(161, 0, 0);

            replacements.Add(new ValueReplacement("W", "oWo", true));
            replacements.Add(new ValueReplacement("l", "w"));
            replacements.Add(new ValueReplacement("L", "W"));
            replacements.Add(new ValueReplacement("r", "w"));
            replacements.Add(new ValueReplacement("R", "W"));
            replacements.Add(new ValueReplacement("n", "ny"));
            replacements.Add(new ValueReplacement("N", "Ny"));
            replacements.Add(new ValueReplacement("you", "U", true));
        }
    }
}
