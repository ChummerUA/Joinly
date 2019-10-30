using Prism.Common;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        #region variables
        private ShellContent CurrentShellContent => CurrentItem?.CurrentItem?.CurrentItem;
        private NavigationParameters Parameters { get; set; }
        #endregion 

        public AppShell()
        {
            InitializeComponent();
        }

        #region override

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            try
            {
                var vmResult = Prism.Mvvm.ViewModelLocator.GetAutowireViewModel(LastPage());
                if (vmResult == null)
                    Prism.Mvvm.ViewModelLocator.SetAutowireViewModel(LastPage(), true);

                PageUtilities.OnNavigatedTo(LastPage(), Parameters);
                Parameters = null;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EXCEPTION: {this?.GetType()?.Name}.OnNavigated() \n{ex.Message}\n{ex}");
            }
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
            try
            {
                PageUtilities.OnNavigatedFrom(LastPage(), Parameters);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"EXCEPTION: {this?.GetType()?.Name}.OnNavigated() \n{ex.Message}\n{ex}");
            }
        }

        #endregion

        #region methods

        public static async Task GoToAsync(string route, NavigationParameters navParams = null)
        {
            (Shell.Current as AppShell).Parameters = navParams;
            await Shell.Current.GoToAsync(route);
        }

        private Page LastPage()
        {
            if(CurrentItem == null)
            {
                return null;
            }
            try
            {
                return CurrentItem?.CurrentItem?.Stack?.Count == 1 ? CurrentShellContent.Content as Page : CurrentItem.CurrentItem.Stack?.LastOrDefault();
            }
            catch
            {
                return null;
            }
        }
        
        #endregion
    }
}