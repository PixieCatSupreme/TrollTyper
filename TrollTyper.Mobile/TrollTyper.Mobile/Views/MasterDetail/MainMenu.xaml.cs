using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TrollTyper.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrollTyper.Mobile.Views.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        public ListView ListView;

        public MainMenu()
        {
            InitializeComponent();

            BindingContext = new MasterDetailMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailMenuItem> MenuItems { get; set; }

            public MasterDetailMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailMenuItem>(new[]
                {
                    new MasterDetailMenuItem { Title = "Apply quirks", TargetType = typeof(TranslatorPage) },
                    new MasterDetailMenuItem { Title = "Manage quirks", TargetType = typeof(QuirkPage) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}