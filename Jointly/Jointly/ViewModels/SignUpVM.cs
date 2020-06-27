using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models.Domain;
using Jointly.Resources;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class SignUpVM : BaseVM
    {
        #region variables
        private SignUpModel _signUpModel;
        public SignUpModel SignUpModel
            => _signUpModel ??= new SignUpModel();

        private bool _isSuccess;
        public bool IsSuccess
        {
            get => _isSuccess;
            set => SetProperty(ref _isSuccess, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
        #endregion

        #region services
        private readonly IUserService UserService;
        #endregion

        #region commands
        private ICommand _signUpCommand;
        public ICommand SignUpCommand =>
          _signUpCommand ??= new Command(SignUp);
        #endregion

        public SignUpVM(
            INavigationService navService,
            IPopupService popupService,
            IUserService userService,
            IAnalyticsService analyticsService) : base(
                navService,
                popupService,
                analyticsService)
        {
            UserService = userService;
        }

        #region methods

        private void SignUp()
        {
            if (IsBusy)
                return;

            //TODO: error titles and validation
            if (!SignUpModel.Email.Validate(Constants.Validation.EmailRegex))
            {
                PopupService.ShowAlert("", Localization.Error_Email);
                return;
            }

            if (!SignUpModel.Phone.Validate(Constants.Validation.PhoneRegex))
            {
                PopupService.ShowAlert("", Localization.Error_Phone);
                return;
            }

            IsBusy = true;

            Task.Run(async () =>
            {
                try
                {
                    await UserService.SignUpAsync(SignUpModel);
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
        #endregion
    }
}
