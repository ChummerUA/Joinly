using Jointly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Jointly.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SetEventPage : ContentPage
    {
        private SetEventViewModel ViewModel { get; set; }

        public SetEventPage()
        {
            InitializeComponent();

            ViewModel = new SetEventViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            var position = new Position(50.619900, 26.251617);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(500)));
            //EventPin.Position = new Position(50.619900, 26.251617);
            base.OnAppearing();
        }
    }
}