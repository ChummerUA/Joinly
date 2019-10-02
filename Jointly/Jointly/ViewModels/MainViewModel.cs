using Jointly.Models;
using Jointly.Controls;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Maps;
using Jointly.Interfaces;

namespace Jointly.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService, IPopupService popupService) : base(navigationService, popupService)
        {
        }
    }
}
