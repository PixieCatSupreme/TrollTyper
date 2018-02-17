using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.Mobile.ViewModels;
using TrollTyper.Quirks.Typing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrollTyper.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TranslatorPage : ContentPage
	{
        TranslatorViewModel viewModel;
        public TranslatorPage ()
		{
			InitializeComponent ();

            for (ShowNameMode i = 0; i != ShowNameMode.END; i++)
            {
                nameModePicker.Items.Add(i.ToString());
            }

            viewModel = new TranslatorViewModel();
            BindingContext = viewModel;

            Appearing += TranslatorPage_Appearing;

        }

        private void TranslatorPage_Appearing(object sender, EventArgs e)
        {
            viewModel.GetNames();
        }
    }
}