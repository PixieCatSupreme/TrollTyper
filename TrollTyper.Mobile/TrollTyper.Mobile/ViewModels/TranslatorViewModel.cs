using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrollTyper.Mobile.Models;
using TrollTyper.Quirks.Typing;
using Xamarin.Forms;


namespace TrollTyper.Mobile.ViewModels
{
    public class TranslatorViewModel
    {
        private Converter _converter;

        private Command _applyQuirksCommand;
        public Command ApplyQuirksCommand
        {
            get
            {
                return _applyQuirksCommand;
            }
        }
        public bool IsBbcMode { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public TranslatorViewModel()
        {
            _converter = new Converter();
            _applyQuirksCommand = new Command(() => ApplyQuirks());
        }



        public void ApplyQuirks()
        {
            string text = Input;
            _converter.ConvertText(ref text, IsBbcMode);
            Output = text;
        }
    }
}
