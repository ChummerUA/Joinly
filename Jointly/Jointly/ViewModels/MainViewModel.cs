using Jointly.Models;
using Jointly.Controls;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace Jointly.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
