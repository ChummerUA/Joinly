using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Interfaces
{
    public interface IPopupService
    {
        // Open a PopupPage
        Task PushAsync(PopupPage page, bool animate = true);

        // Close the last PopupPage int the PopupStack
        Task PopAsync(bool animate = true);

        // Close all PopupPages in the PopupStack
        Task PopAllAsync(bool animate = true);

        // Close an one PopupPage in the PopupStack even if the page is not the last
        Task RemovePageAsync(PopupPage page, bool animate = true);

        void ShowAlert(string title, string message, string cancel = "Ok");
    }
}
