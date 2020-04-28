using Jointly.Models;
using Jointly.Controls;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Jointly.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Jointly.ViewModels
{
    public class MainViewModel : BaseVM
    {
        #region Variables


        private Stage _current;
        public Stage Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }


        #endregion

        #region Commands
        private ICommand _newEventCommand;

        public ICommand NewEventCommand => _newEventCommand ??= new Command(NewEvent);
        private ICommand _confirmEventCommand;
        public ICommand ConfirmEventCommand => _confirmEventCommand ??= new Command(async() => await ConfirmEventAsync());
        #endregion

        public MainViewModel(
            INavigationService navigationService, 
            IPopupService popupService) : base(navigationService, popupService)
        {
            Current = Stage.Map;
        }

        #region methods

        private void NewEvent()
        {
            Current = Stage.NewEvent;
        }

        private async Task ConfirmEventAsync()
        {
            Current = Stage.Map;
        }

        #endregion
    }

    public enum Stage
    {
        Map,
        NewEvent,
        EventInfo
    }
}
