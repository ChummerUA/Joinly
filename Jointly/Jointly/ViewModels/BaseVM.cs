using Jointly.Interfaces;
using Jointly.Models.Responses;
using Prism.AppModel;
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
    public class BaseVM : BindableBase, IInitialize, INavigationAware, IDestructible, IApplicationLifecycleAware, IPageLifecycleAware
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

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        #endregion

        #region services
        protected readonly IPopupService PopupService;
        protected readonly INavigationService NavigationService;
        protected readonly IAnalyticsService AnalyticsService;
        #endregion

        public BaseVM(
            INavigationService navigationService, 
            IPopupService popupService,
            IAnalyticsService analyticsService)
        {
            NavigationService = navigationService;
            PopupService = popupService;
            AnalyticsService = analyticsService;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnAppearing()
        {
            IsActive = true;
        }

        public virtual void OnDisappearing()
        {
            IsActive = false;
        }

        public virtual void Destroy()
        {
            
        }

        public virtual void OnResume()
        {
            IsActive = true;
        }

        public virtual void OnSleep()
        {
            IsActive = false;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
            
        }
        
        protected virtual async void Back(INavigationParameters parameters = null)
        {
            parameters ??= new NavigationParameters();
            await NavigationService.GoBackAsync(parameters);
        }

        protected bool SetBusy(bool busy)
        {
            var changed = busy != IsBusy;
            IsBusy = busy;
            return changed;
        }
    }
}
