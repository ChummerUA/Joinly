using Android.Content;
using Jointly.Controls;
using Jointly.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Material.Android;

[assembly: ExportRenderer(typeof(PasswordEntry), typeof(PasswordEntryRenderer))]
namespace Jointly.Droid.Renderers
{
    public class PasswordEntryRenderer : MaterialEntryRenderer
    {
        public PasswordEntryRenderer(Context context) : base(context)
        {
        }

        protected override MaterialFormsTextInputLayout CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            control.PasswordVisibilityToggleEnabled = true;
            return control;
        }
    }
}