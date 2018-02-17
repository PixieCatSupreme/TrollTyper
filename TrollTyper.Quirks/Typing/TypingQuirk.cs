using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TrollTyper.Quirks.Logging;
using TrollTyper.Quirks.Scripting;

using static TrollTyper.Quirks.Scripting.Utilities;

namespace TrollTyper.Quirks.Typing
{
    public class TypingQuirk
    {
        public string ChatHandle { get; private set; }
        public string ChatHandleShort { get; private set; }

        public string FullHandle
        {
            get
            {
                return $"{ChatHandleShort}: {ChatHandle}";
            }
        }

        public Color ChatColor { get; set; }

        public string Name { get; set; }

        protected List<ValueReplacement> replacements;

        private Script _script;
        private DynValue _preQuirk;
        private DynValue _postQuirk;

        public TypingQuirk(string test)
        {
            ChatHandle = "noneYet";
            ChatHandleShort = "XX";
            ChatColor = Color.Black;
            replacements = new List<ValueReplacement>();
        }

        public TypingQuirk(Script script)
        {
            _script = script;
            ChatHandle = _script.Globals.Get("chatHandle").String;
            ChatHandleShort = $"{Char.ToUpper(ChatHandle[0])}{GetFirstCapitalized(ChatHandle)}";

            Name = _script.Globals.Get("name").String;
            ChatColor = (Color)_script.Globals.Get("chatColor").ToObject();

            replacements = TableToList(_script.Globals.Get("replacements").Table);

            DynValue preQuirk = _script.Globals.Get("PreQuirk");

            DynValue postQuirk = _script.Globals.Get("PostQuirk");

            _preQuirk = preQuirk.Type != DataType.Nil ? preQuirk : null;
            _postQuirk = postQuirk.Type != DataType.Nil ? postQuirk : null;
        }

        private List<ValueReplacement> TableToList(Table table)
        {
            List<ValueReplacement> l = new List<ValueReplacement>();

            for (int i = 0; i < table.Length; i++)
            {
                l.Add(table.Values.ElementAt(i).ToObject<ValueReplacement>());
            }
            return l;
        }

        public virtual bool ApplyQuirk(string text, bool isBbcMode, out string output)
        {
            output = "";
            if (_preQuirk != null)
            {
                try
                {
                    text = _script.Call(_preQuirk, text).String;
                }
                catch (Exception ex)
                {
                    Logger.WriteException(ex);
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
                    output = _script.Call(_postQuirk, text).String;
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.WriteException(ex);
                    return false;
                }
            }
            else
            {
                output = text;
                return true;
            }
        }

        private char GetFirstCapitalized(string text)
        {
            char c = ' ';
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    c = text[i];
                    break;
                }
            }
            return c;
        }
    }
}
