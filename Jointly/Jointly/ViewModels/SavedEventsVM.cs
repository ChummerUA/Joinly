using Jointly.Interfaces;
using Jointly.Models.Domain;
using Jointly.Pages;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class SavedEventsVM : BaseVM
    {
        private ObservableCollection<Event> _events;
        public ObservableCollection<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        private readonly IEventsService EventsService;

        private ICommand _createCommand;
        public ICommand CreateCommand =>
            _createCommand ??= new Command(() => CreateEvent());

        public SavedEventsVM(
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
                Events = new ObservableCollection<Event>(await EventsService.GetSavedEventsAsync());
            }
            catch (Exception ex)
            {
                //Message
            }
        }

        private async void CreateEvent()
        {
            if (!SetBusy(true))
                return;
            await NavigationService.NavigateAsync(nameof(CreateEventPage));
            IsBusy = false;
        }
    }
}
