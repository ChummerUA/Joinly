using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Controls
{
    public class CustomCollectionView : CollectionView
    {
        public static BindableProperty PaddingProperty = BindableProperty.Create(
            nameof(Padding),
            typeof(Thickness),
            typeof(CustomCollectionView),
            default,
            BindingMode.OneWay);

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
    }
}
