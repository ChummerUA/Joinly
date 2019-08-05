using Jointly.Models;
using Jointly.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public enum AuthorizationTypes
    {
        SignIn,
        SignUp
    };

    public class AuthorizationViewModel : BaseViewModel
    {
        #region Properties
        protected IAuthorizationService AuthorizationService { get; }

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

        bool isBusy;
        private bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }        
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
            IsBusy = false;
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
                    Message = "На вказаний E-mail надіслано посилання для активації вашого облікового запису!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    IsSuccess = false;
                    Message = "Помилка! Щось пішло не так!";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    IsSuccess = false;
                    Message = "Такий користувач вже існує! Спробуйте іншу адресу та телефон";
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
}
