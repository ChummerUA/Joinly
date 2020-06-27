using Jointly.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.ViewModels
{
    public class MainVM : BaseVM
    {
        public MainVM(
            INavigationService navigationService, 
            IPopupService popupService, 
            IAnalyticsService analyticsService) : base(
                navigationService, 
                popupService, 
                analyticsService)
        {
        }
    }
}
