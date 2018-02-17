using System.Linq;
using System.Text.RegularExpressions;

namespace TrollTyper.Quirks.Scripting
{
    public class Utilities
    {
        public const string nextLines = "\r\n|\r|\n";
        public const string specialMessageOpener = "--";

        public static char[] seperator = new char[] { ':' };
        public static char[] vowels = new char[] { 'a', 'e', 'i', 'u', 'o' };
        public static char[] punctuations = new char[] { '.', ',', '!', '?' };

        public static Color CreateColor(int R, int G, int B)
        {
            return new Color(R, G, B);
        }
        public static ValueReplacement CreateReplacement(string oldValue, string newValue, bool ignoreCase = false, bool htmlModeOnly = false)
        {
            return new ValueReplacement(oldValue, newValue, ignoreCase, htmlModeOnly);
        }


        public static int GetSeed(string text)
        {
            return text.GetHashCode();
        }

        public static string[] SplitWords(string text)
        {
            var test = text.Substring(0).Split(' ');
            return text.Substring(0).Split(' ');
        }

        public static string[] SplitSentences(string text)
        {
            return Regex.Split(text, @"(?<=[.,;!?])");
        }

        public static string RemovePunctuations(string text)
        {
            for (int i = 0; i < punctuations.Length; i++)
            {
                text = text.Replace($"{punctuations[i]}", "");
            }
            return text;
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

        public static bool IsNullOrWhiteSpace(string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsPunctuation(string character)
        {
            return character.Length > 0 && punctuations.Any(p => p == character[0]);
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
            return character.Length > 0 && vowels.Any(v => v == character[0]);
        }
    }
}
