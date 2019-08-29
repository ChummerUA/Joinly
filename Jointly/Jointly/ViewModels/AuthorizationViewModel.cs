using Jointly.Interfaces;
using Jointly.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        #region Properties

        SignInModel signInModel;
        public SignInModel SignInModel
        {
            get => signInModel;
            set => SetProperty(ref signInModel, value);
        }

        SignUpModel signUpModel;
        public SignUpModel SignUpModel
        {
            get => signUpModel;
            set => SetProperty(ref signUpModel, value);
        }

        AuthorizationTypes authType;
        public AuthorizationTypes AuthType
        {
            get => authType;
            set => SetProperty(ref authType, value);
        }

        string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        bool isSuccess;
        public bool IsSuccess
        {
            get => isSuccess;
            set => SetProperty(ref isSuccess, value);
        }
        #endregion

        #region services
        protected IAuthorizationService AuthorizationService { get; }

        #endregion

        #region Commands
        Command _changeAuthTypeCommand;
        public Command ChangeAuthTypeCommand
        {
            get => _changeAuthTypeCommand;
            set => SetProperty(ref _changeAuthTypeCommand, value);
        }

        Command _authCommand;
        public Command AuthCommand
        {
            get => _authCommand;
            set => SetProperty(ref _authCommand, value);
        }
        #endregion

        public AuthorizationViewModel(IAuthorizationService authorizationService, INavigationService navigationService) : base(navigationService)
        {
            AuthorizationService = authorizationService;

            AuthCommand = new Command(async () => await AuthAsync());
            ChangeAuthTypeCommand = new Command(ChangeAuthType);

            SignInModel = new SignInModel();
            SignUpModel = new SignUpModel();

            AuthType = AuthorizationTypes.SignIn;
            Message = "";
            IsSuccess = false;
        }
        
        #region Metohds
        private async Task AuthAsync()
        {
            switch (AuthType)
            {
                case AuthorizationTypes.SignIn:
                    {
                        await SignInAsync();
                        break;
                    }
                case AuthorizationTypes.SignUp:
                    {
                        await SignUpAsync();
                        break;
                    }
            }
        }

        private async Task SignUpAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                var response = await AuthorizationService.SignUpAsync(SignUpModel);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    IsSuccess = true;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    IsSuccess = false;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    IsSuccess = false;
                }

                IsBusy = false;
            });
        }

        private async Task SignInAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                var response = await AuthorizationService.SignInAsync(SignInModel);

                if (response.IsSuccessStatusCode)
                {
                    await NavigationService.NavigateAsync("MainPage");
                }

                IsBusy = false;
            });
        }

        private void ChangeAuthType()
        {
            if (IsBusy)
            {
                return;
            }
            switch (AuthType)
            {
                case AuthorizationTypes.SignIn:
                    AuthType = AuthorizationTypes.SignUp;
                    break;
                case AuthorizationTypes.SignUp:
                    AuthType = AuthorizationTypes.SignIn;
                    break;
                default:
                    break;
            }
        }
        #endregion

    }

    public enum AuthorizationTypes
    {
        SignIn,
        SignUp
    };

}
