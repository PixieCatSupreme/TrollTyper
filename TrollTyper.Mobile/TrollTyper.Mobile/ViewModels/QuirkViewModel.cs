using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrollTyper.Mobile.Models;
using TrollTyper.Quirks.Typing;
using Xamarin.Forms;


namespace TrollTyper.Mobile.ViewModels
{
    public class QuirkViewModel
    {
        public ObservableCollection<TypingQuirk> Quirks { get; set; }

        public QuirkViewModel()
        {
            Quirks = new ObservableCollection<TypingQuirk>();
        }

        private Command _loadQuirksCommand;
        public Command LoadQuirksCommand
        {
            get
            {
                if (_loadQuirksCommand == null)
                {
                    _loadQuirksCommand = new Command(() => LoadQuirks());
                }
                return _loadQuirksCommand;
            }
        }

        private void LoadQuirks()
        {
            Quirks = new ObservableCollection<TypingQuirk>();
            string luaD = "chatHandle = \"huntressAnimal\" name = \"Mitina\" chatColor = Color.Burgundy replacements = {	  ValueReplacement(\"W\", \"oWo\", true),	  ValueReplacement(\"l\", \"w\"),	  ValueReplacement(\"L\", \"W\"),	  ValueReplacement(\"r\", \"w\"),	  ValueReplacement(\"R\", \"W\"),	  ValueReplacement(\"n\", \"ny\"),	  ValueReplacement(\"N\", \"Ny\"),	  ValueReplacement(\"you\", \"U\", true)}function PostQuirk(text)	words = Utilities.SplitWords(text);	sentenceLenght = Utilities.GetArrayLenght(words) 		text = \"\"		for i = 0, sentenceLenght -1, 1 do	    word = words[i]		char = string.sub(word, 1, 1)		if char ~= \"w\" and Utilities.IsVowel(string.sub(word, 2, 2)) then			text = text .. \" \" .. char .. \"w\" .. string.sub(word, 2, string.len(word))		else			text = text .. \" \" .. word		end	end	return text end";
            TypingQuirk mitina = QuirkLoader.LoadQuirk(luaD);

            if (mitina != null)
            {
                Quirks.Add(mitina);
            }
        }
    }
}
