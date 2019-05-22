using Jointly.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jointly.ViewModels
{
    public class SetEventViewModel : INotifyPropertyChanged
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

        EventModel @event;

        public EventModel Event
        {
            get { return @event; }
            set
            {
                if(@event != value)
                {
                    @event = value;
                    OnPropertyChanged("Event");
                }
            }
        }

        public SetEventViewModel()
        {
            var begin = DateTime.Now.TimeOfDay;
            var end = begin + new TimeSpan(1, 0, 0);
            
            Event = new EventModel
            {
                EventName = "",
                EventInfo = "",
                Date = DateTime.Today,
                BeginningTime = new TimeSpan(begin.Hours, begin.Minutes, begin.Seconds),
                EndingTime = new TimeSpan(end.Hours, end.Minutes, end.Seconds),
                Position = new Xamarin.Forms.Maps.Position()
            };
        }
    }
}
