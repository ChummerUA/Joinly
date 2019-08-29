using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService NavigationService { get; }

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

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public void Init(INavigationParameters parameters)
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public void OnNavigatingTo(INavigationParameters parameters)
        {
        }
    }
}
