using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrollTyper.Quirks.Logging;
using TrollTyper.Quirks.Scripting;
using static TrollTyper.Quirks.Scripting.Utilities;

namespace TrollTyper.Quirks.Typing
{
    public class Converter
    {
        public List<TypingQuirk> TypingQuirks { get; set; }
        private StringBuilder _sb;
        private bool _isBbcMode;

        public Converter()
        {
            TypingQuirks = new List<TypingQuirk>();
            _sb = new StringBuilder();
            _isBbcMode = false;
        }

        public bool TryGetName(string name, out TypingQuirk quirk)
        {
            quirk = TypingQuirks.FirstOrDefault(q => q.Name == name);

            return quirk != null;
        }


        public bool TryGetShortName(string chatHandleShort, out TypingQuirk quirk)
        {
            quirk = TypingQuirks.FirstOrDefault(q => q.ChatHandleShort == chatHandleShort);

            return quirk != null;
        }

        public bool ConvertText(ref string text, bool isBbcMode)
        {
            string[] lines = Regex.Split(text, nextLines);
            string[] splitString;

            string line;

            _isBbcMode = isBbcMode;

            _sb.Clear();

            if (isBbcMode)
            {
                _sb.Append("[spoiler open=\"Open Pesterlog\" close=\"Close Pesterlog\"][left]");
            }

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i];
                if (!string.IsNullOrWhiteSpace(line))
                {
                    splitString = line.Split(seperator, 2);

                    if (TryGetShortName(splitString[0], out TypingQuirk quirk))
                    {
                        if (!ConvertChatMessage(splitString[1], true, quirk))
                        {
                            return false;
                        }
                    }
                    else if (TryGetName(splitString[0], out quirk))
                    {
                        if (!ConvertChatMessage(splitString[1], false, quirk))
                        {
                            return false;
                        }
                    }
                    else if (line.StartsWith(specialMessageOpener))
                    {
                        ConvertStartAndEndMessage(line);
                    }
                    else
                    {
                        Logger.WriteWarn("No quirk found for character with userHandle: " + splitString[0] + "!");
                        return false;
                    }

                    if (i + 1 < lines.Length)
                    {
                        _sb.AppendLine();
                    }
                }
            }

            if (isBbcMode)
            {
                _sb.Append("[/left][/spoiler]");
            }

            text = _sb.ToString();
            return true;
        }

        private void ConvertStartAndEndMessage(string text)
        {
            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                TypingQuirk quirk = TypingQuirks.FirstOrDefault(q => q.ChatHandleShort == words[i].Trim(punctuations));

                _sb.Append(' ');

                if (quirk != null)
                {
                    char lastChar = words[i].Last();
                    SetChatName(quirk, punctuations.Any(p => p == lastChar) ? new char?(lastChar) : null);
                }
                else
                {
                    _sb.Append(words[i]);
                }
            }
        }

        private void SetChatName(TypingQuirk quirk, char? punctiation)
        {
            _sb.Append($"{quirk.ChatHandle} ");

            if (_isBbcMode)
            {
                _sb.Append($"[color=#{ColorToHex(quirk.ChatColor)}]");
            }

            _sb.Append($"[{quirk.ChatHandleShort}]");

            if (_isBbcMode)
            {
                _sb.Append("[/color]");
            }

            if (punctiation != null)
            {
                _sb.Append(punctiation);
            }
        }

        private bool ConvertChatMessage(string text, bool isChatName, TypingQuirk quirk)
        {
            if (_isBbcMode)
            {
                Color c = quirk.ChatColor;
                _sb.Append($"[color=#{ColorToHex(quirk.ChatColor)}]");
            }

            _sb.Append(isChatName ? quirk.ChatHandleShort : quirk.Name);
            _sb.Append(seperator[0]);

            if (quirk.ApplyQuirk(text, _isBbcMode, out text))
            {
                _sb.Append(text);

                if (_isBbcMode)
                {
                    _sb.Append("[/color]");
                }
                return true;
            }
            return false;
        }
    }
}
