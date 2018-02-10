using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TrollTyper.Mobile.Models;
using TrollTyper.Quirks.Typing;
using Xamarin.Forms;


namespace TrollTyper.Mobile.ViewModels
{
    public class QuirkViewModel : INotifyPropertyChanged
    {
        private static QuirkViewModel _viewModel;
        public static QuirkViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new QuirkViewModel();
                }
                return _viewModel;
            }
        }


        public ObservableCollection<TypingQuirk> Quirks { get; set; }
        public bool IsBusy { get; set; }

        private QuirkViewModel()
        {
            IsBusy = false;
            Quirks = new ObservableCollection<TypingQuirk>();

            _loadQuirksCommand = new Command(() => LoadQuirks());
        }

        private Command _loadQuirksCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command LoadQuirksCommand
        {
            get
            {
                return _loadQuirksCommand;
            }
        }

        private void LoadQuirks()
        {
            IsBusy = true;
            Quirks.Clear();
            string luaD = "chatHandle = \"huntressAnimal\" name = \"Mitina\" chatColor = Color.Burgundy replacements = {	  TT.CreateReplacement(\"W\", \"oWo\", true),	  TT.CreateReplacement(\"l\", \"w\"),	  TT.CreateReplacement(\"L\", \"W\"),	  TT.CreateReplacement(\"r\", \"w\"),	  TT.CreateReplacement(\"R\", \"W\"),	  TT.CreateReplacement(\"n\", \"ny\"),	  TT.CreateReplacement(\"N\", \"Ny\"),	  TT.CreateReplacement(\"you\", \"U\", true)}function PostQuirk(text)	words = Utilities.SplitWords(text);	sentenceLenght = Utilities.GetArrayLenght(words) 		text = \"\"		for i = 0, sentenceLenght -1, 1 do	    word = words[i]		char = string.sub(word, 1, 1)		if char ~= \"w\" and Utilities.IsVowel(string.sub(word, 2, 2)) then			text = text .. \" \" .. char .. \"w\" .. string.sub(word, 2, string.len(word))		else			text = text .. \" \" .. word		end	end	return text end";
            TypingQuirk mitina = QuirkLoader.LoadQuirk(luaD);

            if (mitina != null)
            {
                Quirks.Add(mitina);
            }

            IsBusy = false;
            OnPropertyChanged("");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
