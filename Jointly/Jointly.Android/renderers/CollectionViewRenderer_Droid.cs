using Android.Content;
using Android.Graphics;
using Jointly.Controls;
using Jointly.Droid.Renderers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(CustomCollectionView), typeof(CollectionViewRenderer_Droid))]
namespace Jointly.Droid.Renderers
{
    public class CollectionViewRenderer_Droid : CollectionViewRenderer
    {
        private double Density => DeviceDisplay.MainDisplayInfo.Density;

        public CollectionViewRenderer_Droid(Context context) : base(context)
        {
        }

        protected override ItemDecoration CreateSpacingDecoration(IItemsLayout itemsLayout)
        {
            var rect = new Rect();
            if (Element is CustomCollectionView collectionView)
            {
                rect.Top = (int)(collectionView.Padding.Top * Density);
                rect.Bottom = (int)(collectionView.Padding.Bottom * Density);
                rect.Left = (int)(collectionView.Padding.Left * Density);
                rect.Right = (int)(collectionView.Padding.Right * Density);
            }
            return new CollectionItemDecoration(itemsLayout, rect);
        }
    }
}