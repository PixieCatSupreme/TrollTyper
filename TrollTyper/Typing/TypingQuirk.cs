using NLua;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using TrollTyper.Scripting;

using static TrollTyper.Scripting.Utilities;

namespace TrollTyper
{
    public class TypingQuirk
    {
        public string ChatHandle { get; private set; }
        public string ChatHandleShort { get; private set; }
        public Color ChatColor { get; set; }
        public string Name { get; set; }

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

            Name = script.GetString("name");
            ChatColor = (Color)script["chatColor"];

            replacements = script.GetTableDict(script.GetTable("replacements")).Select(k => k.Value as ValueReplacement).ToList();

            _preQuirk = script.GetFunction("PreQuirk");
            _postQuirk = script.GetFunction("PostQuirk");
        }

        public virtual bool ApplyQuirk(string text, bool isBbcMode, out string output)
        {
            output = "";
            if (_preQuirk != null)
            {
                try
                {
                    text = _preQuirk.Call(text)[0] as string;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }

            for (int i = 0; i < replacements.Count; i++)
            {
                ValueReplacement vr = replacements[i];
                text = Regex.Replace(text, vr.OldValue, vr.NewValue, vr.IgnoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            }

            if (_postQuirk != null)
            {
                try
                {
                    output = _postQuirk.Call(text)[0] as string;
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                output = text;
                return true;
            }
        }
    }
}
