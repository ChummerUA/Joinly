using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Jointly.Droid.Effects;
using Jointly.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Jointly")]
[assembly: ExportEffect(typeof(RoundCorners_Droid), "RoundCorners")]
namespace Jointly.Droid.Effects
{
    public class RoundCorners_Droid : PlatformEffect
    {
        private double density => Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
        private Color _color;

        private Xamarin.Forms.View View { get; set; }

        protected override void OnAttached()
        {
            try
            {
                var effect = Element.Effects.FirstOrDefault(x => x.GetType() == typeof(RoundCorners)) as RoundCorners;

                View = Element as Xamarin.Forms.View;
                _color = View.BackgroundColor;
                View.SizeChanged += View_SizeChanged;

                SetCorners();

            }
            catch { }
        }

        private void SetCorners()
        {
            var draw = new GradientDrawable();
            draw.SetColor(_color.ToAndroid());

            if (Control != null && View.Height > 0)
            {
                var radius = (View.Height / 2) * density;
                draw.SetCornerRadius(Math.Min((float)radius, 60));
                View.BackgroundColor = Color.Transparent;
                Control.Background = draw;
            }
        }

        private void View_SizeChanged(object sender, EventArgs e)
        {
            SetCorners();
        }

        protected override void OnDetached()
        {
            View.SizeChanged -= View_SizeChanged;
        }
    }
}