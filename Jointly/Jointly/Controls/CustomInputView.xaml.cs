using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomInputView : ContentView
    {
        #region bindableProperties
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomInputView),
            "",
            BindingMode.TwoWay);

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(CustomInputView),
            "",
            BindingMode.OneWay);

        public static readonly BindableProperty IsPasswordEntryProperty = BindableProperty.Create(
            nameof(IsPasswordEntry),
            typeof(bool),
            typeof(CustomInputView),
            false,
            BindingMode.OneWay);
        #endregion

        #region variables
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool IsPasswordEntry
        {
            get => (bool)GetValue(IsPasswordEntryProperty);
            set => SetValue(IsPasswordEntryProperty, value);
        }
        #endregion

        public CustomInputView()
        {
            InitializeComponent();
        }
    }
}