using Jointly.Interfaces;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jointly.Services
{
    public class PopupService : IPopupService
    {
        private IPopupNavigation Instance => PopupNavigation.Instance;

        public async Task PushAsync(PopupPage page, bool animate = true)
        {
            await Instance.PushAsync(page, animate);
        }

        public async Task PopAsync(bool animate = true)
        {
            await Instance.PopAsync(animate);
        }

        public async Task PopAllAsync(bool animate = true)
        {
            await Instance.PopAllAsync(animate);
        }

        public async Task RemovePageAsync(PopupPage page, bool animate = true)
        {
            if (PopupNavigation.Instance.PopupStack.Contains(page))
            {
                await Instance.RemovePageAsync(page, animate);
            }
        }

        public void ShowAlert(string title, string message, string cancel = "Ok")
        {
            Device.BeginInvokeOnMainThread(async() => await App.Current.MainPage.DisplayAlert(title, message, cancel));
        }
    }
}
