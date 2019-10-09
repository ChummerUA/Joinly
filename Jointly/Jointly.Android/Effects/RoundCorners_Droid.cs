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

        protected override void OnAttached()
        {
            try
            {
                var effect = Element.Effects.FirstOrDefault(x => x.GetType() == typeof(RoundCorners)) as RoundCorners;
                var radius = effect?.CornerRadius;

                var draw = new GradientDrawable();
                draw.SetCornerRadius((float)(radius * density));
                draw.SetColor((Element as VisualElement).BackgroundColor.ToAndroid());

                if (Control != null)
                {
                    Control.Background = draw;
                }
                if (Container != null)
                {
                    Container.Background = draw;
                }
            }
            catch { }
        }

        protected override void OnDetached()
        {
            
        }
    }
}