using Jointly.Interfaces;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Services
{
    public class PopupService : IPopupService
    {
        public async Task PushAsync(PopupPage page, bool animate = true)
        {
            await PopupNavigation.Instance.PushAsync(page, animate);
        }

        public async Task PopAsync(bool animate = true)
        {
            await PopupNavigation.Instance.PopAsync(animate);
        }

        public async Task PopAllAsync(bool animate = true)
        {
            await PopupNavigation.Instance.PopAllAsync(animate);
        }

        public async Task RemovePageAsync(PopupPage page, bool animate = true)
        {
            await PopupNavigation.Instance.RemovePageAsync(page, animate);
        }

        public async Task ShowAlert(string title, string message, string cancel = "Ok")
        {
            await App.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        public Task DisplayActionSheet()
        {
            throw new NotImplementedException();
        }
    }
}
