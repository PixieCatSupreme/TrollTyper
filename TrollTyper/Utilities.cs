using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper
{
    public static class Utilities
    {
        public static char[] seperator = new char[] { ':' };
        public static char[] vowels = new char[] { 'a', 'e', 'i', 'u', 'o' };
        public static string nextLines = "\r\n|\r|\n";

        public static string ColorToHex(Color color)
        {
            return string.Format("{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }

        public static bool IsVowel(char character)
        {
            return vowels.Any(v => v == character);
        }
    }
}
