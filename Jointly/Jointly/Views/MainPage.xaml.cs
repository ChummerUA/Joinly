using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        #region ICommand
        public ICommand SetEventCommand { get; }
        #endregion

        public MainPage()
        {
            InitializeComponent();

            SetEventCommand = new Command(async () => await SetEventAsync());

            SetEventButton.Command = SetEventCommand;
        }

        private async Task SetEventAsync()
        {
            await Navigation.PushAsync(new SetEventPage());
        }
    }
}