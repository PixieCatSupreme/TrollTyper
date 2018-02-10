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
        private QuirkViewModel viewModel;

        public TranslatorPage ()
		{
			InitializeComponent ();
            viewModel = QuirkViewModel.ViewModel;
            BindingContext = viewModel;
        }
	}
}