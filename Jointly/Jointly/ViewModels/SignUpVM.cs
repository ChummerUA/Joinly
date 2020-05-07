using Jointly.Extensions;
using Jointly.Interfaces;
using Jointly.Models;
using Jointly.Resources;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
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
        {
            get => _signUpModel;
            set => SetProperty(ref _signUpModel, value);
        }

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
            IUserService userService) : 
            base(navService, 
                popupService)
        {
            UserService = userService;
        }

        #region methods

        private void SignUp()
        {
            if (IsBusy)
                return;

            //TODO: error titles and validation
            if (!SignUpModel.Email.Validate(Constants.EmailRegex))
            {
                PopupService.ShowAlert("", Localization.Error_Email);
                return;
            }

            if (!SignUpModel.Phone.Validate(Constants.PhoneRegex))
            {
                PopupService.ShowAlert("", Localization.Error_Phone);
                return;
            }

            IsBusy = true;

            Task.Run(async () =>
            {
                try
                {
                    var result = await UserService.SignUpAsync(SignUpModel);
                    if (result.IsSuccess)
                    {
                        Device.BeginInvokeOnMainThread(() => IsSuccess = true);
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
        #endregion
    }
}
