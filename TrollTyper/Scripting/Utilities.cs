using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace TrollTyper.Scripting
{
    public static class Utilities
    {
        public const string nextLines = "\r\n|\r|\n";
        public const string specialMessageOpener = "--";

        public static char[] seperator = new char[] { ':' };
        public static char[] vowels = new char[] { 'a', 'e', 'i', 'u', 'o' };

        public static Color CreateColor(int R, int G, int B)
        {
            return Color.FromArgb(R, G, B);
        }

        public static int GetSeed(string text)
        {
            return text.GetHashCode();
        }

        public static string[] SplitWords(string text)
        {
            return text.Substring(1).Split(' ');
        }

        public static string[] SplitSentences(string text)
        {
            return Regex.Split(text, @"(?<=[.,;!?])");
        }


        public static int CountWords(string text)
        {
            text = text.Trim();
            int currentWordCount = 0;

            for (int i = 0; i < text.Length;)
            {
                while (i < text.Length && !char.IsWhiteSpace(text[i]))
                {
                    i++;
                }

                currentWordCount++;

                while (i < text.Length && char.IsWhiteSpace(text[i]))
                {
                    i++;
                }
            }
            return currentWordCount;
        }

        public static bool IsNullOrWhiteSpace(string sentence)
        {
            return string.IsNullOrWhiteSpace(sentence);
        }

        public static int GetArrayLenght(object[] array)
        {
            return array.Length;
        }

        public static string ColorToHex(Color color)
        {
            return string.Format("{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }

        public static bool IsVowel(string character)
        {
            return vowels.Any(v => v == character[0]);
        }
    }
}
