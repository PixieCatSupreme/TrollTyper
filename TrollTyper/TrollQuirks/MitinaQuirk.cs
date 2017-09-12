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

            replacements.Add(new KeyValuePair<string, string>("W", "oWo"));
            replacements.Add(new KeyValuePair<string, string>("l", "w"));
            replacements.Add(new KeyValuePair<string, string>("a", "aw"));
            replacements.Add(new KeyValuePair<string, string>("r", "w"));
            replacements.Add(new KeyValuePair<string, string>("p", "pw"));
            replacements.Add(new KeyValuePair<string, string>("you", "u"));
        }
    }
}
