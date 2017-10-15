using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TrollTyper
{
    public class TypingQuirk
    {
        public string ChatHandle { get; private set; }
        public string ChatHandleShort { get; private set; }
        public Color chatColor;
        protected List<ValueReplacement> replacements;

        public TypingQuirk(string chatHandle)
        {
            this.ChatHandle = chatHandle;
            ChatHandleShort = $"{Char.ToUpper(chatHandle[0])}{chatHandle.FirstOrDefault(c => char.IsUpper(c))}";
            chatColor = Color.Black;
            replacements = new List<ValueReplacement>();
        }

        public virtual string ApplyQuirk(string text, bool isBbcMode)
        {
            for (int i = 0; i < replacements.Count; i++)
            {
                ValueReplacement vr = replacements[i];
                text = Regex.Replace(text, vr.OldValue, vr.NewValue, vr.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            }

            return text;
        }

        protected int CountWords(string text)
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
    }
}
