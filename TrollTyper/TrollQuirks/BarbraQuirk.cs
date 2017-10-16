using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.TrollQuirks
{
    class BarbraQuirk : TypingQuirk
    {
        private const int WordStutterScalar = 2;

        public BarbraQuirk() : base("cardridgeCollector")
        {
            chatColor = System.Drawing.Color.FromArgb(7, 132, 70);
        }

        public override string ApplyQuirk(string text, bool isBbcMode)
        {
            Random r = new Random(text.GetHashCode());

            string[] words = text.Substring(1).Split(' ');

            for (int i = 0; i < words.Length / WordStutterScalar; i++)
            {
                int wordIndex = r.Next(0, words.Length);
                string word = words[wordIndex];

                string stutter = $"{word[0]}{ (word.Length > 1 && Utilities.IsVowel(word[1]) ? word[1].ToString() : "")}-";
                words[wordIndex] = $"{stutter}{word}";
            }

            text = "";

            for (int i = 0; i < words.Length; i++)
            {
                text += $" {words[i]}";
            }
            return base.ApplyQuirk(text, isBbcMode);
        }

    }
}
