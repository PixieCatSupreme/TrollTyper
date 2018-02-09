using System;
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

            var logManager = new Common.Logging.LogManager();


            //Quirks.Logging.Logger.Initialize(logManager);

            MainPage = new MasterDetail();
        }
    }
}