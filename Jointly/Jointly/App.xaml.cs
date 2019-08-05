using Jointly.Services;
using Jointly.ViewModels;
using Jointly.Views;
using Jointly.Views.Pages;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly
{
    public partial class App : PrismApplication
    {
        public readonly string ApiURL = @"https://dev.jointly.space";

        public App() : this(null){ }

        public App(IPlatformInitializer platformInitializer) : base(platformInitializer) { }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAuthorizationService, AuthorizationService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<AuthorizationPage, AuthorizationViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            containerRegistry.RegisterForNavigation<SetEventPage, SetEventViewModel>();
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync("AuthorizationPage");
        }
    }
}
