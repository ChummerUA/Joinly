using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Controls
{
    public class RoundedButton : Button
    {
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == nameof(Height))
                FixCorners();
        }

        private void FixCorners()
        {
            CornerRadius = (int)(Height / 2);
        }
    }
}
