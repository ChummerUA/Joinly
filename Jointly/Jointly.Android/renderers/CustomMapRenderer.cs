using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Jointly.Droid.Renderers;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Jointly.Views;
using Jointly.Views.CustomUI;
using Jointly.Views.Pages;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Jointly.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        public CustomMapRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                NativeMap.MarkerClick -= Marker_Clicked;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.MarkerClick += Marker_Clicked;
        }

        private void Marker_Clicked(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            Marker marker = e.Marker;
            var position = new Position(marker.Position.Latitude, marker.Position.Longitude);

            var map = Element as Xamarin.Forms.Maps.Map;
            var page = map.Parent.Parent as MainPage;

            //page.ShowEventInfo(position);
        }
    }
}