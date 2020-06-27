using BruTile.Predefined;
using BruTile.Web;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Rendering.Skia;
using Mapsui.UI;
using System.Linq;
using Xamarin.Forms;

namespace Jointly.Pages
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            var map = new Mapsui.Map
            {
                CRS = "EPSG:3857",
                Transformation = new MinimalTransformation(),
            };

            var tileSource = new HttpTileSource(
                new GlobalSphericalMercator(),
                "http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
                new[] { "a", "b", "c" }, name: "OpenStreetMap");
            var tileLayer = new TileLayer(tileSource) { Name = "OpenStreetMap" };

            //map.Layers.Add(tileLayer);

            //var resolutions = map.Resolutions.OrderBy(x => x);
            //var min = resolutions.FirstOrDefault();
            //var max = resolutions.LastOrDefault() * 0.2f;

            //map.Limiter = new ViewportLimiterKeepWithin
            //{
            //    ZoomLimits = new MinMax(min, max)
            //};

            mapView.Map = map;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            mapView.Refresh();
        }

        private void MapView_Info(object sender, Mapsui.UI.MapInfoEventArgs e)
        {
            if (e?.MapInfo?.Feature != null)
            {
                foreach (var style in e.MapInfo.Feature.Styles)
                {
                    if (style is CalloutStyle)
                    {
                        style.Enabled = !style.Enabled;
                        e.Handled = true;
                    }
                }

                mapView.Refresh();
            }
        }
    }
}
