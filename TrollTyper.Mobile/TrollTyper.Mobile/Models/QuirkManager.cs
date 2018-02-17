using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrollTyper.Quirks.Typing;

namespace TrollTyper.Mobile.Models
{
    public static class QuirkManager
    {
        public static void LoadQuirks()
        {
            Quirks.Typing.QuirkManager.TypingQuirks = new ObservableCollection<TypingQuirk>();
            string luaD = "chatHandle = \"huntressAnimal\"\nname = \"Mitina\"\n\nchatColor = Color.Burgundy\n\nreplacements = \n{\n	  TT.CreateReplacement(\"W\", \"oWo\", true),\n	  TT.CreateReplacement(\"l\", \"w\"),\n	  TT.CreateReplacement(\"L\", \"W\"),\n	  TT.CreateReplacement(\"r\", \"w\"),\n	  TT.CreateReplacement(\"R\", \"W\"),\n	  TT.CreateReplacement(\"n\", \"ny\"),\n	  TT.CreateReplacement(\"N\", \"Ny\"),\n	  TT.CreateReplacement(\"you\", \"U\", true)\n}\n\nfunction PostQuirk(text)\n	words = TT.SplitWords(text);\n	sentenceLenght = TT.GetArrayLenght(words) \n	\n	text = \"\"\n	\n	for i = 1, sentenceLenght, 1 do\n	    word = words[i]\n		char = string.sub(word, 1, 1)\n\n		if char ~= \"w\" and TT.IsVowel(string.sub(word, 2, 2)) then\n			text = text .. \" \" .. char .. \"w\" .. string.sub(word, 2, string.len(word))\n		else\n			text = text .. \" \" .. word\n		end\n	end\n\n	return text\nend\n";
            TypingQuirk mitina = Quirks.Typing.QuirkManager.LoadQuirk(luaD);

            if (mitina != null)
            {
                Quirks.Typing.QuirkManager.TypingQuirks.Add(mitina);
            }
        }
    }
}
