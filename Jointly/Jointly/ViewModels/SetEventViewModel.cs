﻿using Jointly.Interfaces;
using Jointly.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jointly.ViewModels
{
    public class SetEventViewModel : BaseViewModel
    {
        EventModel @event;

        public EventModel Event
        {
            get => @event;
            set => SetProperty(ref @event, value);
        }

        public SetEventViewModel(
            INavigationService navigationService, 
            IPopupService popupService) : base(navigationService, popupService)
        {
            var begin = DateTime.Now.TimeOfDay;
            var end = begin + new TimeSpan(1, 0, 0);
            
            Event = new EventModel
            {
                EventName = "",
                EventInfo = "",
                Date = DateTime.Today,
                BeginningTime = new TimeSpan(begin.Hours, begin.Minutes, begin.Seconds),
                EndingTime = new TimeSpan(end.Hours, end.Minutes, end.Seconds),
                Position = new Xamarin.Forms.Maps.Position()
            };
        }
    }
}
