using NLua;
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
        public Color ChatColor { get; set; }

        protected List<ValueReplacement> replacements;

        private LuaFunction _preQuirk;
        private LuaFunction _postQuirk;

        public TypingQuirk(string test)
        {
            ChatHandle = "noneYet";
            ChatHandleShort = "XX";
            ChatColor = Color.Black;
            replacements = new List<ValueReplacement>();
        }

        public TypingQuirk(Lua script)
        {
            ChatHandle = script.GetString("chatHandle");
            ChatHandleShort = $"{Char.ToUpper(ChatHandle[0])}{ChatHandle.FirstOrDefault(c => char.IsUpper(c))}";
            ChatColor = script.GetTable("chatColor").ToColor();

            replacements = script.GetTableDict(script.GetTable("replacements")).Select(k => k.Value as ValueReplacement).ToList();

            _preQuirk = script.GetFunction("PreQuirk");
            _postQuirk = script.GetFunction("PostQuirk");
        }

        public virtual string ApplyQuirk(string text, bool isBbcMode)
        {
            if (_preQuirk != null)
            {
                _preQuirk.Call(text);
            }

            for (int i = 0; i < replacements.Count; i++)
            {
                ValueReplacement vr = replacements[i];
                text = Regex.Replace(text, vr.OldValue, vr.NewValue, vr.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            }

            if (_postQuirk != null)
            {
                return _postQuirk.Call(text)[0] as string;
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
