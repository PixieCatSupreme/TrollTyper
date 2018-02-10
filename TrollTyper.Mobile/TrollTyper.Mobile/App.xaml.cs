using System;
using TrollTyper.Mobile.ViewModels;
using TrollTyper.Mobile.Views.MasterDetail;
using Xamarin.Forms;

namespace TrollTyper.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            var logger = Common.Logging.LogManager.GetLogger<App>();

            Quirks.Logging.Logger.Initialize(logger);

            MainPage = new MasterDetail();

            QuirkViewModel.ViewModel.LoadQuirksCommand.Execute(null);
        }
    }
}