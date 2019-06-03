using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Jointly.CustomUI
{
    public class CustomMap : Map
    {
        public Page Parent
        {
            get { return (Page)GetValue(ParentProperty); }
            set
            {
                SetValue(ParentProperty, value);
            }
        }

        public CustomPin SelectedEventPin
        {
            get { return (CustomPin)GetValue(SelectedEventPinProperty); }
            set
            {
                SetValue(SelectedEventPinProperty, value);
                OnPropertyChanged("SelectedEventPin");
                OnPropertyChanged("IsEventInfoShown");
            }
        }

        public static readonly BindableProperty SelectedEventPinProperty =
            BindableProperty.Create("SelectedEventPin", typeof(CustomPin), typeof(CustomMap));

        public static readonly BindableProperty ParentProperty =
            BindableProperty.Create("Parent", typeof(Page), typeof(CustomMap));

        public bool IsEventInfoShown
        {
            get
            {
                if (SelectedEventPin == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void SelectPin(CustomPin pin)
        {
            SelectedEventPin = pin;
        }
    }
}
