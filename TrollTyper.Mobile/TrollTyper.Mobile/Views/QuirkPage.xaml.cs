using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TrollTyper.Mobile.Models;

namespace TrollTyper.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuirkPage : ContentPage
	{
        QuirkViewModel viewModel;
        public QuirkPage ()
		{
			InitializeComponent ();
            viewModel = new QuirkViewModel();
            BindingContext = viewModel;
        }

        private async void AddQuirkClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new EditQuirkPage()));
        }

        protected void OnItemSelected()
        {

        }
    }
}