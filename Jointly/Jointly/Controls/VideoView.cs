using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Controls
{
    public class VideoView : View
    {
        public static readonly BindableProperty SourceProperty =
            BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(VideoView),
            string.Empty,
            BindingMode.TwoWay);

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly BindableProperty LoopProperty =
            BindableProperty.Create(
            nameof(Loop),
            typeof(bool),
            typeof(VideoView),
            true,
            BindingMode.TwoWay);

        public bool Loop
        {
            get { return (bool)GetValue(LoopProperty); }
            set { SetValue(LoopProperty, value); }
        }

        public Action OnFinishedPlaying { get; set; }
    }
}
