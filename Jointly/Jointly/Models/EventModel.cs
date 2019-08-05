using Prism.Mvvm;
using System;
using Xamarin.Forms.Maps;

namespace Jointly.Models
{
    public class EventModel : BindableBase
    {
        #region variables
        string eventName;
        string eventInfo;
        DateTime date;
        TimeSpan beginningTime;
        TimeSpan endingTime;
        Position position;
        #endregion

        #region Properties
        public string EventName
        {
            get => eventName;
            set => SetProperty(ref eventName, value);
        }

        public string EventInfo
        {
            get => eventInfo;
            set => SetProperty(ref eventInfo, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public TimeSpan BeginningTime
        {
            get => beginningTime;
            set => SetProperty(ref beginningTime, value);
        }

        public TimeSpan EndingTime
        {
            get => endingTime;
            set => SetProperty(ref endingTime, value);
        }

        public Position Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }
        #endregion
    }
}
