using Jointly.Interfaces;
using Jointly.Models.Domain;
using Jointly.Resources;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class CreateEventVM : BaseVM
    {
        private Event _event;
        public Event Event
            => _event ??= new Event();

        private readonly IEventsService EventsService;


        private ICommand _createCommand;
        public ICommand CreateCommand =>
          _createCommand ??= new Command(() => CreateEvent());


        public CreateEventVM(
            INavigationService navigationService, 
            IPopupService popupService, 
            IAnalyticsService analyticsService,
            IEventsService eventsService) : base(navigationService, popupService, analyticsService)
        {
            EventsService = eventsService;
        }

        private async void CreateEvent()
        {
            if (!SetBusy(true))
                return;
            try
            {
                await EventsService.CreateEventAsync(Event);
                var parameters = new NavigationParameters
                {
                    { Constants.Navigation.Created, true }
                };
                Back(parameters);
            }
            catch(Exception ex)
            {
                //Message
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
