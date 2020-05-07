using Jointly.Interfaces;
using Jointly.Pages;
using Jointly.Services;
using Jointly.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Net.Http;

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
            });
            var client = services.BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("DefaultClient");

            containerRegistry.RegisterSingleton<IAnalyticsService, AnalyticsService>();
            containerRegistry.RegisterInstance(client);
            containerRegistry.RegisterSingleton<IPreferencesService, PreferencesService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IPopupService, PopupService>();

            containerRegistry.RegisterForNavigation<SignInPage, SignInVM>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpVM>();
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync(nameof(SignInPage));
        }
    }
}
