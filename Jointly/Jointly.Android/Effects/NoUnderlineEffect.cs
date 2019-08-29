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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Jointly")]
[assembly: ExportEffect(typeof(NoUnderlineEffect), "NoUnderlineEffect")]
namespace Jointly.Droid.Effects
{
    class NoUnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                Control.Background = null;
            }
            catch
            {

            }
        }

        protected override void OnDetached()
        {

        }
    }
}