using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        #region Properties

        private SignInModel _signInModel;
        public SignInModel SignInModel
        {
            get => _signInModel;
            set => SetProperty(ref _signInModel, value);
        }

        private SignUpModel _signUpModel;
        public SignUpModel SignUpModel
        {
            get => _signUpModel;
            set => SetProperty(ref _signUpModel, value);
        }

        private AuthorizationTypes _authType;
        public AuthorizationTypes AuthType
        {
            get => _authType;
            set => SetProperty(ref _authType, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private bool _isSuccess;
        public bool IsSuccess
        {
            get => _isSuccess;
            set => SetProperty(ref _isSuccess, value);
        }
        #endregion

        #region services
        private readonly IUserService UserService;

        #endregion

        #region Commands
        private ICommand _changeAuthTypeCommand;

        public ICommand ChangeAuthTypeCommand => _changeAuthTypeCommand ??= new Command(ChangeAuthType);

        private ICommand _authCommand;
        public ICommand AuthCommand => _authCommand ??= new Command(async () => await AuthAsync());
        #endregion

        public AuthorizationViewModel(
            IUserService userService,
            IPopupService popupService,
            INavigationService navigationService) : base(navigationService, popupService)
        {
            UserService = userService;

            SignInModel = new SignInModel();
            SignUpModel = new SignUpModel();

            AuthType = AuthorizationTypes.SignIn;
            IsSuccess = false;
        }

        #region override

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            IsBusy = false;
        }

        #endregion

        #region methods
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
            if(
                !(await SignUpModel.Username.Validate(Localization.Localization.Error_Username)) ||
                !(await SignUpModel.Email.Validate(Localization.Localization.Error_Email)) ||
                !(await SignUpModel.Phone.Validate(Localization.Localization.Error_Phone)))
            {
                return;
            }

            IsBusy = true;

            var result = await UserService.SignUpAsync(SignUpModel);
            if (result.IsSuccess)
            {
                IsSuccess = true;
            }
            else
            {
                await PopupService.ShowAlert(Localization.Localization.Error, result.Message);
            }

            IsBusy = false;
        }

        private async Task SignInAsync()
        {
            //await NavigationService.NavigateAsync("AppShell");
            App.Current.MainPage = new AppShell();
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
