using Jointly.Models;
using Jointly.Controls;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Maps;
using Jointly.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Jointly.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Variables

        private Stage _current;
        public Stage Current
        {
            get { return _current; }
            set { SetProperty(ref _current, value); }
        }

        #endregion

        #region Commands
        private ICommand _newEventCommand;
        public ICommand NewEventCommand
        {
            get { return _newEventCommand = _newEventCommand ?? new Command(NewEvent); }
        }

        private ICommand _confirmEventCommand;
        public ICommand ConfirmEventCommand
        {
            get { return _confirmEventCommand = _confirmEventCommand ?? new Command(ConfirmEvent); }
        }
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

        private void ConfirmEvent()
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
