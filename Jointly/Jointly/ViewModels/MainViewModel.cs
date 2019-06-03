using Jointly.CustomUI;
using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace Jointly.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;
            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        CustomPin selectedEventPin;
        ObservableCollection<CustomPin> pins;

        public CustomPin SelectedEventPin
        {
            get { return selectedEventPin; }
            set
            {
                if (selectedEventPin != value)
                {
                    selectedEventPin = value;
                    OnPropertyChanged("SelectedEventPin");
                }
            }
        }
        public ObservableCollection<CustomPin> Pins
        {
            get { return pins; }
            set
            {
                if(pins != value)
                {
                    pins = value;
                    OnPropertyChanged("Pins");
                }
            }
        }

        public MainViewModel()
        {
            Pins = new ObservableCollection<CustomPin>
            {
                new CustomPin(new EventModel
                {
                    EventName = "Test",
                    EventInfo = "TestInfo",
                    Date = DateTime.Today,
                    BeginningTime = DateTime.Now.TimeOfDay,
                    EndingTime = DateTime.Now.TimeOfDay,
                    Position = new Xamarin.Forms.Maps.Position(50.619900, 26.251617)
                })
            };
            //SelectedEvent = Pins[0].Event;
        }
    }
}
