using Jointly.Interfaces;
using Jointly.Models.Domain;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Jointly.ViewModels
{
    public class MapVM : BaseVM
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
            => _events ??= new ObservableCollection<Event>();

        private readonly IEventsService EventsService;

        public MapVM(
            INavigationService navigationService, 
            IPopupService popupService, 
            IAnalyticsService analyticsService,
            IEventsService eventsService) : base(
                navigationService, 
                popupService, 
                analyticsService)
        {
            EventsService = eventsService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            try
            {
                //Events
            }
            catch (Exception ex)
            {
                //Message
            }
        }
    }
}
