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

        public bool IsBusy { get; set; }

        public ObservableCollection<TypingQuirk> TypingQuirks
        {
            get
            {
                return Quirks.Typing.QuirkManager.TypingQuirks;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadQuirks()
        {
            IsBusy = true;
            Models.QuirkManager.LoadQuirks();
            IsBusy = false;
            OnPropertyChanged("");
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
