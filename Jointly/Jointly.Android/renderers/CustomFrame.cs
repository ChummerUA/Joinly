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
using Jointly.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Java.Util.ResourceBundle;

[assembly: ExportRenderer(typeof(Jointly.Renderers.CustomFrame), typeof(Jointly.Droid.Renderers.CustomFrame))]
namespace Jointly.Droid.Renderers
{
    class CustomFrame : FrameRenderer
    {
        public CustomFrame(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var drawable = new GradientDrawable();
            
            drawable.SetCornerRadius(10);
            drawable.SetStroke(1, Android.Graphics.Color.Black);
            SetBackgroundDrawable(drawable);
        }
    }
}