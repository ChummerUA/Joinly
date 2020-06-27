using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Pages;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class ProfileVM : BaseVM
    {
        private User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private readonly IUserService UserService;

        private ICommand _logoutCommand;
        public ICommand LogoutCommand =>
            _logoutCommand ??= new Command(() => Logout());

        public ProfileVM(
            INavigationService navigationService, 
            IPopupService popupService, 
            IAnalyticsService analyticsService,
            IUserService userService) : base(
                navigationService, 
                popupService, 
                analyticsService)
        {
            UserService = userService;
        }

        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            try
            {
                //TODO initialize
                IsInitialized = true;
            }
            catch(Exception ex)
            {
                AnalyticsService.TrackError(ex);
            }
        }

        private async void Logout()
        {
            UserService.Logout();
            await NavigationService.NavigateAsync($"/{nameof(SignInPage)}");
        }
    }
}
