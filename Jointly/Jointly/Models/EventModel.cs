using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Maps;

namespace Jointly.Models
{
    public class EventModel : INotifyPropertyChanged
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

        #region variables
        string eventName;
        string eventInfo;
        DateTime date;
        TimeSpan beginningTime;
        TimeSpan endingTime;
        Position position;
        #endregion

        public string EventName
        {
            get { return eventName; }
            set
            {
                if (eventName != value)
                {
                    eventName = value;
                    OnPropertyChanged("EventName");
                }
            }
        }

        public string EventInfo
        {
            get { return eventInfo; }
            set
            {
                if(eventInfo != value)
                {
                    eventInfo = value;
                    OnPropertyChanged("EventInfo");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if(date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public TimeSpan BeginningTime
        {
            get { return beginningTime; }
            set
            {
                if(beginningTime != value)
                {
                    beginningTime = value;
                    OnPropertyChanged("BeginningTime");
                }
            }
        }

        public TimeSpan EndingTime
        {
            get { return endingTime; }
            set
            {
                if(endingTime != value)
                {
                    endingTime = value;
                    OnPropertyChanged("EndingTime");
                }
            }
        }

        public Position Position
        {
            get { return position; }
            set
            {
                if(position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
    }
}
