using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jointly.Views.CustomUI
{
    public class CustomPin : Pin
    {
        public EventModel Event
        {
            get { return (EventModel)GetValue(EventProperty); }
            set
            {
                SetValue(EventProperty, value);
            }
        }

        public static readonly BindableProperty EventProperty =
            BindableProperty.Create("Event", typeof(EventModel), typeof(CustomPin));

        public CustomPin(EventModel @event) : base()
        {
            Event = @event;
            Position = @event.Position;
        }
    }
}
