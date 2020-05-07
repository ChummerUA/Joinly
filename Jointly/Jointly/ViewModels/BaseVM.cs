using Jointly.Interfaces;
using Jointly.Models.Responses;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class BaseVM : BindableBase, INavigationAware
    {
        #region variables
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _isInitialized;
        public bool IsInitialized
        {
            get => _isInitialized;
            set => SetProperty(ref _isInitialized, value);
        }
        #endregion

        #region services
        protected readonly IPopupService PopupService;
        protected readonly INavigationService NavigationService;
        #endregion

        public BaseVM(INavigationService navigationService, IPopupService popupService)
        {
            NavigationService = navigationService;
            PopupService = popupService;
        }

        #region virtual methods
        public virtual async Task Init(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            IsBusy = true;
            if (!IsInitialized)
            {
                Task.Run(async() => await Init(parameters));
            }
        }

        protected virtual void OnAPIRequestFailed(BaseResponse response)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
            });
        }
        #endregion
    }
}
