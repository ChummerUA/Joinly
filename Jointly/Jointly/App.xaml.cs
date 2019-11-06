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

namespace Jointly
{
    public partial class App : PrismApplication
    {
        public readonly string ApiURL = @"https://dev.jointly.space";

        public App() : this(null){ }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer)
        {
            //var localize = ServiceResolver.Get<ILocalize>();
            //var ci = localize.GetCurrentCultureInfo();
            //localize.SetLocale(ci);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IApiService, ApiService>();
            containerRegistry.RegisterSingleton<IUserService, UserService>();
            containerRegistry.RegisterSingleton<IPopupService, PopupService>();

            containerRegistry.RegisterForNavigation<AuthorizationPage, AuthorizationViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            //containerRegistry.RegisterShellRoute<MainPage, MainViewModel>();
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync("AuthorizationPage");
        }
    }
}
