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
using Xamarin.Forms.Maps.Android;
using Jointly.CustomUI;
using Jointly.Droid.Renderers;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Platform.Android;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Graphics.Drawables;
using Jointly.Views;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace Jointly.Droid.Renderers
{
    class CustomMapRender : MapRenderer
    {
        public CustomMapRender(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
        }

        protected override void OnMapReady(GoogleMap map)
        {
            map.MarkerClick += Map_MarkerClick;
            base.OnMapReady(map);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            
            var bitmapDraw = BitmapFactory.DecodeResource(Resources, Resource.Drawable.pin_blue);
            var bitmap = Bitmap.CreateScaledBitmap(bitmapDraw, 75, 75, false);
            marker.SetIcon(BitmapDescriptorFactory.FromBitmap(bitmap));

            return marker;
        }

        private void Map_MarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            var position = new Position(e.Marker.Position.Latitude, e.Marker.Position.Longitude);
            var map = Element as CustomMap;

            map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMeters(500)));

            map.SelectPin(map.Pins.FirstOrDefault(x => x.Position == position) as CustomPin);
            (map.Parent as MainPage).ShowEventInfo();
        }
    }
}