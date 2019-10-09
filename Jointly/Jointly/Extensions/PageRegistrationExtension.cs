using Jointly.ViewModels;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Jointly.Extensions
{
    public static class PageRegistrationExtension
    {
        public static void RegisterShellRoute<TPage, TViewModel>(this IContainerRegistry containerRegistry) where TPage : ContentPage where TViewModel : BaseViewModel
        {
            Routing.RegisterRoute((typeof(TPage)).Name, typeof(TPage));
            containerRegistry.RegisterForNavigation<TPage, TViewModel>();
        }
    }
}
