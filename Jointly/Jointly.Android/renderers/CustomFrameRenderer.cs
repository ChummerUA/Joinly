using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Jointly.CustomUI;
using Xamarin.Forms;
using Jointly.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace Jointly.Droid.Renderers
{
    class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var drawable = new GradientDrawable();

            drawable.SetCornerRadius(e.NewElement.CornerRadius);
            drawable.SetColor(e.NewElement.BackgroundColor.ToAndroid().ToArgb());
            drawable.SetStroke(2, e.NewElement.BorderColor.ToAndroid());
            SetBackgroundDrawable(drawable);
        }
    }
}