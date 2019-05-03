using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Renderers
{
    public class CustomFrame : Frame
    {
        //public static readonly BindableProperty BorderThicknessProperty =
        //    BindableProperty.Create<CustomFrame, double>(p => p.BorderThickness, default(double));

        //public double BorderThickness
        //{
        //    get { return (double)GetValue(BorderThicknessProperty); }
        //    set { SetValue(BorderThicknessProperty, value); }
        //}
        public double BorderThickness { get; set; }
    }
}
