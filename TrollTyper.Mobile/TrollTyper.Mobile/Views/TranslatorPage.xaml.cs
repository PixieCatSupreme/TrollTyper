using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.Mobile.ViewModels;
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
            viewModel = new TranslatorViewModel();
            BindingContext = viewModel;
        }
	}
}