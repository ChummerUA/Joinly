using Jointly.Interfaces;
using Jointly.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jointly.ViewModels
{
    public class NewEventViewModel : BaseViewModel
    {
        private EventModel _event;
        public EventModel Event
        {
            get { return _event; }
            set { SetProperty(ref _event, value); }
        }

        public NewEventViewModel(
            INavigationService navigationService, 
            IPopupService popupService) : base(navigationService, popupService)
        {
        }
    }
}
