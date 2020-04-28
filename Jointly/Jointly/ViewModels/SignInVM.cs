using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Pages;
using Jointly.Resources;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class SignInVM : BaseVM
    {
        #region variables

        private SignInModel _signInModel;
        public SignInModel SignInModel
        {
            get => _signInModel;
            set => SetProperty(ref _signInModel, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        #endregion

        #region services
        private readonly IUserService UserService;

        #endregion

        #region Commands
        private ICommand _signUpCommand;
        public ICommand SignUpCommand =>
          _signUpCommand ??= new Command(async() => await GoToSignUpAsync());

        private ICommand _signInCommand;
        public ICommand SignInCommand =>
          _signInCommand ??= new Command(SignIn);
        #endregion

        public SignInVM(
            IUserService userService,
            IPopupService popupService,
            INavigationService navigationService) : base(navigationService, popupService)
        {
            UserService = userService;

            SignInModel = new SignInModel();
        }

        #region override

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsBusy = false;
        }

        #endregion

        #region methods

        private void SignIn()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Task.Run(async () =>
            {
                try
                {
                    var result = await UserService.SignInAsync(SignInModel);
                    if (result.IsSuccess)
                    {
                        //TODO: Navigate to MainPage
                    }
                    else
                    {
                        PopupService.ShowAlert(Localization.Error, result.Message);
                    }
                }
                catch (Exception ex)
                {
                    //TODO: exception handling
                }
                finally
                {
                    Device.BeginInvokeOnMainThread(() => IsBusy = false);
                }
            });
        }

        private async Task GoToSignUpAsync()
        {
            if (IsBusy)
                return;
            try
            {
                await NavigationService.NavigateAsync(nameof(SignUpPage));
            }
            catch(Exception ex)
            {
                //TODO: exception handling
            }
        }
        #endregion
    }
}
