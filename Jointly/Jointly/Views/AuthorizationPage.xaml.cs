using GalaSoft.MvvmLight.Command;
using Jointly.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jointly.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage : ContentPage
    {
        AuthorizationViewModel AuthorizationViewModel { get; set; }

        public AuthorizationPage()
        {
            InitializeComponent();
            AuthorizationViewModel = new AuthorizationViewModel();
            BindingContext = AuthorizationViewModel;
        }

        #region Execute
        private void ChangeAuthentificationType(object sender, EventArgs e)
        {
            if (!AuthorizationViewModel.ChangeAuthTypeCommand.CanExecute(null))
            {
                return;
            }

            AuthorizationViewModel.IsMessageVisible = false;

            if (AuthorizationViewModel.AuthType == "SignIn")
            {
                SignInStack.IsVisible = true;
                SignUpStack.IsVisible = false;
                UsernameEntry.Text = "";
                EmailEntry.Text = "";
                PhoneEntry.Text = "";
            }
            else if (AuthorizationViewModel.AuthType == "SignUp")
            {
                SignInStack.IsVisible = false;
                SignUpStack.IsVisible = true;
                LoginEntry.Text = "";
                PasswordEntry.Text = "";
            }
        }
        #endregion

        #region Events
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            AuthorizationViewModel.IsMessageVisible = false;

            var entry = sender as Entry;
            entry.ReturnCommand.Execute(e.NewTextValue);

            (ConfirmSignUpButton.Command as RelayCommand).RaiseCanExecuteChanged();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            var entry = sender as Entry;

            if(AuthorizationViewModel.AuthType == "SignIn")
            {
                if(entry.Placeholder == "Ім'я користувача")
                {
                    PasswordEntry.Focus();
                }
            }
            else if(AuthorizationViewModel.AuthType == "SignUp")
            {
                if (entry.Placeholder == "Ім'я користувача")
                {
                    EmailEntry.Focus();
                }
                else if(entry.Placeholder == "Пошта")
                {
                    PhoneEntry.Focus();
                }
            }
        }

        private async void SignIn_Async(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new MainPage());
        }
        #endregion
    }
}