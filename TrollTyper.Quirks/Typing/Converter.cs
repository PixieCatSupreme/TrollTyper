using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private StringBuilder _sb;
        private bool _isBbcMode;

        public Converter()
        {
            _sb = new StringBuilder();
            _isBbcMode = false;
        }

        public bool TryGetName(string name, out TypingQuirk quirk)
        {
            quirk = QuirkManager.TypingQuirks.FirstOrDefault(q => q.Name == name);

            return quirk != null;
        }


        public bool TryGetShortName(string chatHandleShort, out TypingQuirk quirk)
        {
            quirk = QuirkManager.TypingQuirks.FirstOrDefault(q => q.ChatHandleShort == chatHandleShort);

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
                    splitString = line.Split(seperator, 2).Select(l => l.Trim()).ToArray();

                    if (TryGetShortName(splitString[0], out TypingQuirk quirk))
                    {
                        if (!ConvertChatMessage(splitString[1], quirk, true))
                        {
                            return false;
                        }
                    }
                    else if (TryGetName(splitString[0], out quirk))
                    {
                        if (!ConvertChatMessage(splitString[1], quirk, false))
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

        public bool ConvertChatMessage(ref string text, TypingQuirk quirk, bool isBbcMode, ShowNameMode showNameMode)
        {
            string[] lines = Regex.Split(text, nextLines).Select(l => l.Trim()).ToArray();
            string line = "";

            _sb.Clear();
            for (int i = 0; i < lines.Length;)
            {
                line = lines[i];
                if (isBbcMode)
                {
                    _sb.Append($"[color=#{ColorToHex(quirk.ChatColor)}]");
                }

                if (showNameMode != ShowNameMode.None)
                {
                    _sb.Append(showNameMode == ShowNameMode.ChatHandle ? quirk.ChatHandleShort : quirk.Name);
                    _sb.Append(seperator[0]);
                }

                if (quirk.ApplyQuirk(line, _isBbcMode, out text))
                {
                    _sb.Append(text);
                }
                else
                {
                    return false;
                }
                i++;

                if (i < lines.Length)
                {
                    _sb.AppendLine();
                }
            }
            if (isBbcMode)
            {
                _sb.Append("[/color]");
            }


            text = _sb.ToString();
            return true;
        }


        private void ConvertStartAndEndMessage(string text)
        {
            string[] words = text.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                TypingQuirk quirk = QuirkManager.TypingQuirks.FirstOrDefault(q => q.ChatHandleShort == words[i].Trim(punctuations));

                _sb.Append(' ');

                if (quirk != null)
                {
                    string word = words[i];
                    char lastChar = word[word.Length - 1];
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

        private bool ConvertChatMessage(string text, TypingQuirk quirk, bool isChatName, bool showChatName = true)
        {
            if (_isBbcMode)
            {
                _sb.Append($"[color=#{ColorToHex(quirk.ChatColor)}]");
            }

            if (showChatName)
            {
                _sb.Append(isChatName ? quirk.ChatHandleShort : quirk.Name);
                _sb.Append(seperator[0]);
            }

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
