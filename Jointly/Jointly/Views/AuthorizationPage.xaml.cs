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

        #region Command
        public ICommand ChangeAuthentificationTypeCommand { get; }
        #endregion

        public AuthorizationPage()
        {
            InitializeComponent();
            AuthorizationViewModel = new AuthorizationViewModel();
            BindingContext = AuthorizationViewModel;

            ChangeAuthentificationTypeCommand = new Command(ChangeAuthentificationType);

            UsernameEntry.ReturnCommand = AuthorizationViewModel.SetUsernameCommand;
            EmailEntry.ReturnCommand = AuthorizationViewModel.SetEmailCommand;
            PhoneEntry.ReturnCommand = AuthorizationViewModel.SetPhoneCommand;

            SignInButton.Command = ChangeAuthentificationTypeCommand;
            SignUpButton.Command = ChangeAuthentificationTypeCommand;

            ConfirmSignUpButton.Command = AuthorizationViewModel.SignUpCommand;
        }

        #region Execute
        private void ChangeAuthentificationType()
        {
            if (AuthorizationViewModel.AuthType == "SignIn")
            {
                SignInStack.IsVisible = false;
                SignUpStack.IsVisible = true;
                AuthorizationViewModel.AuthType = "SignUp";

                UsernameEntry.Text = "";
                EmailEntry.Text = "";
                PhoneEntry.Text = "";
            }
            else if (AuthorizationViewModel.AuthType == "SignUp")
            {
                SignInStack.IsVisible = true;
                SignUpStack.IsVisible = false;
                AuthorizationViewModel.AuthType = "SignIn";
            }
        }
        #endregion

        #region Events
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;

            if (entry.ReturnCommand.CanExecute(entry.Text))
            {
                if (entry.Placeholder == "Ім'я користувача")
                {
                    UsernameInfoLabel.Text = "";
                    UsernameInfoLabel.IsVisible = false;
                }
                else if (entry.Placeholder == "Пошта")
                {
                    EmailInfoLabel.Text = "";
                    EmailInfoLabel.IsVisible = false;
                }
                else if (entry.Placeholder == "Телефон")
                {
                    PhoneInfoLabel.Text = "";
                    PhoneInfoLabel.IsVisible = false;
                }
            }

            if (entry.Placeholder == "Ім'я користувача" && entry.Text == "")
            {
                UsernameInfoLabel.IsVisible = false;
            }
            else if (entry.Placeholder == "Пошта" && entry.Text == "")
            {
                EmailInfoLabel.IsVisible = false;
            }
            else if (entry.Placeholder == "Телефон" && entry.Text == "")
            {
                PhoneInfoLabel.IsVisible = false;
            }

            AuthorizationViewModel.IsMessageVisible = false;
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            var entry = sender as Entry;

            if (!entry.ReturnCommand.CanExecute(entry.Text) && entry.Text != "")
            {
                if (entry.Placeholder == "Ім'я користувача")
                {
                    UsernameInfoLabel.Text = "Невірно введене ім'я користувача";
                    UsernameInfoLabel.IsVisible = true;
                }
                else if (entry.Placeholder == "Пошта")
                {
                    EmailInfoLabel.Text = "Невірно введена пошта";
                    EmailInfoLabel.IsVisible = true;
                }
                else if (entry.Placeholder == "Телефон")
                {
                    PhoneInfoLabel.Text = "Невірно введений номер";
                    PhoneInfoLabel.IsVisible = true;
                }
            }

            entry.ReturnCommand.Execute(entry.Text);
        }
        #endregion
    }
}