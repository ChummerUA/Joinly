using Jointly.Interfaces;
using Jointly.Pages;
using Jointly.Resources;
using Jointly.Services;
using Jointly.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace Jointly
{
    public partial class App : PrismApplication
    {
        public App() : this(null) { }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var services = new ServiceCollection();
            services.AddHttpClient("DefaultClient", _client =>
            {
                _client.Timeout = TimeSpan.FromSeconds(30);
                _client.DefaultRequestHeaders
                    .Accept
                    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                _client.BaseAddress = new Uri(Constants.APIURL);
            });
            var client = services.BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("DefaultClient");

            containerRegistry.RegisterSingleton<IAnalyticsService, AnalyticsService>();
            containerRegistry.RegisterInstance(client);
            containerRegistry.RegisterSingleton<IPreferencesService, PreferencesService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IPopupService, PopupService>();
            containerRegistry.RegisterSingleton<IEventsService, EventsService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainVM>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInVM>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpVM>();
            containerRegistry.RegisterForNavigation<MapPage, MapVM>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfileVM>();
            containerRegistry.RegisterForNavigation<SavedEventsPage, SavedEventsVM>();
            containerRegistry.RegisterForNavigation<CreateEventPage, CreateEventVM>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync(nameof(MainPage));
        }
    }
}
