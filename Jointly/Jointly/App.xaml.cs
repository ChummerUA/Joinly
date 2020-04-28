using Jointly.Interfaces;
using Jointly.Services;
using Jointly.ViewModels;
using Jointly.Pages;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Jointly.Helpers;
using Jointly.Extensions;
using System.Net.Http;
using Jointly.Utils;

namespace Jointly
{
    public partial class App : PrismApplication
    {
        public App() : this(null){ }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAnalyticsService, AnalyticsService>();
            containerRegistry.RegisterSingleton<IPreferencesService, PreferencesService>();
            containerRegistry.RegisterSingleton<APIClient>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IBaseAPI, BaseAPI>();
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
