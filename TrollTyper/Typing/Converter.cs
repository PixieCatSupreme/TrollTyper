using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static TrollTyper.Utilities;

namespace TrollTyper
{
    class Converter
    {
        public TypingQuirk[] TypingQuirks { get; set; }
        private StringBuilder _sb;
        private bool _isBbcMode;

        public Converter(params TypingQuirk[] quirks)
        {
            TypingQuirks = quirks;
            _sb = new StringBuilder();
            _isBbcMode = false;
        }

        public bool TryGetName(string chatHandle, out TypingQuirk quirk)
        {
            quirk = TypingQuirks.FirstOrDefault(q => q.ChatHandle == chatHandle);

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
                        ConvertChatMessage(splitString[1], quirk);
                    }
                    else if (line.Substring(0, line.IndexOf(' ')) == "--")
                    {
                        ConvertStartAndEndMessage(line);
                    }
                    else
                    {
                        Console.WriteLine("No quirk found for character with userHandle: " + splitString[0] + "!");
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
            if (_isBbcMode)
            {
                string[] words = text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    TypingQuirk quirk = TypingQuirks.FirstOrDefault(q => q.ChatHandleShort == words[i]);

                    _sb.Append(' ');

                    if (quirk != null)
                    {
                        SetChatName(quirk);
                    }
                    else
                    {
                        _sb.Append(words[i]);
                    }
                }
            }
            else
            {
                _sb.Append(text);
            }
        }

        private void SetChatName(TypingQuirk quirk)
        {
            _sb.Append($"{quirk.ChatHandle} ");

            if (_isBbcMode)
            {
                _sb.Append($"[color=#{ColorToHex(quirk.chatColor)}]");
            }

            _sb.Append($"[{quirk.ChatHandleShort}]");

            if (_isBbcMode)
            {
                _sb.Append("[/color]");
            }
        }

        private void ConvertChatMessage(string text, TypingQuirk quirk)
        {
            if (_isBbcMode)
            {
                Color c = quirk.chatColor;
                _sb.Append($"[color=#{ColorToHex(quirk.chatColor)}]");
            }

            _sb.Append(quirk.ChatHandleShort);
            _sb.Append(seperator[0]);
            _sb.Append(quirk.ApplyQuirk(text, _isBbcMode));
            if (_isBbcMode)
            {
                _sb.Append("[/color]");
            }
        }
    }
}
