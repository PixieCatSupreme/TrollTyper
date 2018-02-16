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

        public ObservableCollection<TypingQuirk> Quirks
        {
            get
            {
                return QuirkManager.Quirks;
            }
        }

        public void LoadQuirks()
        {
            QuirkManager.LoadQuirks();
        }
    }
}
