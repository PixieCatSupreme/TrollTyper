using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrollTyper.Mobile.Models;
using TrollTyper.Quirks.Typing;
using Xamarin.Forms;


namespace TrollTyper.Mobile.ViewModels
{
    public class TranslatorViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private Command _applyQuirksCommand;

        public Command ApplyQuirksCommand
        {
            get
            {
                return _applyQuirksCommand;
            }
        }

        private Command _savetoClipboardCommand;

        public Command _SavetoClipboardCommand
        {
            get
            {
                return _savetoClipboardCommand;
            }
        }
        private List<string> _quirkNames;
        public List<string> QuirkNames
        {
            get
            {
                if (_quirkNames == null)
                {
                    GetNames();
                }
                return _quirkNames;
            }
            private set
            {
                _quirkNames = value;
                OnPropertyChanged("QuirkNames");
            }
        }

        public bool SelectQuirkMode
        {
            get
            {
                return SelectedQuirkID > 0;
            }
        }
        private bool _isBbcMode;
        public bool IsBbcMode
        {
            get
            {
                return _isBbcMode;
            }
            set
            {
                _isBbcMode = value;
                OnPropertyChanged("IsBbcMode");
            }
        }

        public bool HasOutput
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Output);
            }
        }
        public string Input { get; set; }
        private string _output;
        public string Output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                OnPropertyChanged("Output");
                OnPropertyChanged("HasOutput");
            }
        }

        private int _selectedQuirkID;
        public int SelectedQuirkID
        {
            get
            {
                return _selectedQuirkID;
            }
            set
            {
                _selectedQuirkID = value;
                OnPropertyChanged("SelectedQuirkID");
                OnPropertyChanged("SelectQuirkMode");
            }
        }

        private ShowNameMode _selectedNameMode;
        public int SelectedNameMode
        {
            get
            {
                return (int)_selectedNameMode;
            }
            set
            {
                _selectedNameMode = (ShowNameMode)value;
                OnPropertyChanged("SelectedNameMode");
            }
        }

        private Converter _converter;

        public TranslatorViewModel()
        {
            _converter = new Converter();
            _applyQuirksCommand = new Command(() => ApplyQuirks());
        }


        public void GetNames()
        {
            QuirkNames = new List<string>
            {
                "All quirks"
            };
            QuirkNames.AddRange(Quirks.Typing.QuirkManager.TypingQuirks.Select(t => t.FullHandle).ToList());
            SelectedQuirkID = 0;
            SelectedNameMode = 0;
            OnPropertyChanged("SelectedQuirkID");
        }

        public void ApplyQuirks()
        {
            if (!string.IsNullOrWhiteSpace(Input))
            {
                if (SelectQuirkMode)
                {
                    Output = ApplySelectedQuirk();
                }
                else
                {
                    Output = ApplyAllQuirks();
                }
            }
        }

        private string ApplySelectedQuirk()
        {
            string text = "";

            TypingQuirk quirk = Quirks.Typing.QuirkManager.TypingQuirks.ElementAtOrDefault(SelectedQuirkID - 1);
            if (quirk != null)
            {
                text = Input;
                _converter.ConvertChatMessage(ref text, quirk, IsBbcMode, _selectedNameMode);
            }

            return text;
        }

        private string ApplyAllQuirks()
        {
            string text = Input;
            _converter.ConvertText(ref text, IsBbcMode);
            return text;
        }

        private void SaveToClipBoardCommand()
        {
//TODO
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
