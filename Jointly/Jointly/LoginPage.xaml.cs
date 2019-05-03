using Jointly.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Jointly
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        static bool IsSubmitButtonClicked { get; set; } = false;

        public LoginPage ()
		{
			InitializeComponent ();

            UsernameFrame.HeightRequest = UsernameEntry.MinimumHeightRequest;
            UsernameFrame.WidthRequest = UsernameEntry.MinimumWidthRequest;

            PasswordFrame.HeightRequest = PasswordEntry.MinimumHeightRequest;
            PasswordFrame.WidthRequest = PasswordEntry.MinimumWidthRequest;

            PasswordVisibilityButton.Clicked += PasswordVisibilityButton_Clicked;
            SubmitButton.Clicked += SubmitButton_Clicked;
            ChangeAuthButton.Clicked += ChangeAuthButton_Clicked;

            var nameGest = new TapGestureRecognizer();
            nameGest.Tapped += UsernameLabel_Tapped;
            var passGest = new TapGestureRecognizer();
            passGest.Tapped += PasswordLabel_Tapped;
            var regNameGest = new TapGestureRecognizer();
            regNameGest.Tapped += RegNameLabel_Tapped;
            var regEmailGest = new TapGestureRecognizer();
            regEmailGest.Tapped += RegEmailLabel_Tapped;
            var regPhoneGest = new TapGestureRecognizer();
            regPhoneGest.Tapped += RegPhoneLabel_Tapped;

            UsernameLabel.GestureRecognizers.Add(nameGest);
            UsernameEntry.Focused += UsernameEntry_Focused;
            UsernameEntry.Unfocused += UsernameEntry_Unfocused;

            PasswordLabel.GestureRecognizers.Add(passGest);
            PasswordEntry.Focused += PasswordEntry_Focused;
            PasswordEntry.Unfocused += PasswordEntry_Unfocused;

            RegNameLabel.GestureRecognizers.Add(passGest);
            RegNameEntry.Focused += RegNameEntry_Focused;
            RegNameEntry.Unfocused += RegNameEntry_Unfocused;

            RegEmailLabel.GestureRecognizers.Add(passGest);
            RegEmailEntry.Focused += RegEmailEntry_Focused;
            RegEmailEntry.Unfocused += RegEmailEntry_Unfocused;

            RegPhoneLabel.GestureRecognizers.Add(passGest);
            RegPhoneEntry.Focused += RegPhoneEntry_Focused;
            RegPhoneEntry.Unfocused += RegPhoneEntry_Unfocused;
        }

        private void PasswordVisibilityButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.IsPassword)
            {
                PasswordVisibilityButton.Image = "round_visibility_black_36dp";
                PasswordEntry.IsPassword = false;
            }
            else
            {
                PasswordVisibilityButton.Image = "round_visibility_off_black_36dp";
                PasswordEntry.IsPassword = true;
            }
        }

        #region Entries Focus
        private async void Entry_Focused(string propName)
        {
            //Hack to show keyboard on android device

            await Task.Run(() => Task.Delay(1));
            var entry = FindByName(propName + "Entry") as Entry;
            var label = FindByName(propName + "Label") as Label;
            var size = entry.FontSize;

            label.IsVisible = false;
            entry.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Entry));
            entry.TextColor = ChangeAuthButton.TextColor;
            entry.Placeholder = "";
        }

        private void Entry_Unfocused(string propName)
        {
            var entry = FindByName(propName + "Entry") as Entry;
            var label = FindByName(propName + "Label") as Label;
            if (entry.Text.Length == 0)
            {
                label.IsVisible = true;
            }
        }

        private void UsernameEntry_Focused(object sender, EventArgs e)
        {
            Entry_Focused("Username");
        }

        private void UsernameEntry_Unfocused(object sender, EventArgs e)
        {
            Entry_Unfocused("Username");
        }

        private void UsernameLabel_Tapped(object sender, EventArgs e)
        {
            Entry_Focused("Username");
        }

        private void PasswordEntry_Focused(object sender, EventArgs e)
        {
            Entry_Focused("Password");
        }

        private void PasswordEntry_Unfocused(object sender, EventArgs e)
        {
            Entry_Unfocused("Password");
        }

        private void PasswordLabel_Tapped(object sender, EventArgs e)
        {
            Entry_Focused("Password");
        }

        private void RegNameEntry_Focused(object sender, EventArgs e)
        {
            Entry_Focused("RegName");
        }

        private void RegNameEntry_Unfocused(object sender, EventArgs e)
        {
            Entry_Unfocused("RegName");
        }

        private void RegNameLabel_Tapped(object sender, EventArgs e)
        {
            Entry_Focused("RegName");
        }

        private void RegEmailEntry_Focused(object sender, EventArgs e)
        {
            Entry_Focused("RegEmail");
        }

        private void RegEmailEntry_Unfocused(object sender, EventArgs e)
        {
            Entry_Unfocused("RegEmail");
        }

        private void RegEmailLabel_Tapped(object sender, EventArgs e)
        {
            Entry_Focused("RegEmail");
        }

        private void RegPhoneEntry_Focused(object sender, EventArgs e)
        {
            Entry_Focused("RegPhone");
        }

        private void RegPhoneEntry_Unfocused(object sender, EventArgs e)
        {
            Entry_Unfocused("RegPhone");
        }

        private void RegPhoneLabel_Tapped(object sender, EventArgs e)
        {
            Entry_Focused("RegPhone");
        }
        #endregion

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            if (IsSubmitButtonClicked)
            {
                return;
            }

            IsSubmitButtonClicked = true;
            
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                InfoLabel.IsVisible = true;
                InfoLabel.Text = "Відсутній доступ до інтернету!";
                InfoLabel.TextColor = Color.Red;
                IsSubmitButtonClicked = false;
                return;
            }
            
            string type = "";
            List<String> props = new List<string>();

            if (SubmitButton.Text == "Увійти")
            {
                type = "Authorization";
                props.Add("Username");
                props.Add("Password");
            }
            else
            {
                type = "Registration";
                props.Add("RegName");
                props.Add("RegEmail");
                props.Add("RegPhone");
            }
            CheckFields(props, type);
        }

        private void ChangeAuthButton_Clicked(object sender, EventArgs e)
        {
            if (IsSubmitButtonClicked)
            {
                return;
            }

            if(SubmitButton.Text == "Увійти")
            {
                SubmitButton.Text = "Зареєструватись";
                ChangeAuthButton.Text = "Увійти";

                UsernameEntry.Text = "";
                PasswordEntry.Text = "";
                UsernameLabel.IsVisible = true;
                PasswordLabel.IsVisible = true;

                UsernameFrame.IsVisible = false;
                PasswordFrame.IsVisible = false;

                RegNameFrame.IsVisible = true;
                RegEmailFrame.IsVisible = true;
                RegPhoneFrame.IsVisible = true;
            }
            else
            {
                SubmitButton.Text = "Увійти";
                ChangeAuthButton.Text = "Зареєструватись";

                RegNameEntry.Text = "";
                RegEmailEntry.Text = "";
                RegPhoneEntry.Text = "";
                RegNameLabel.IsVisible = true;
                RegEmailLabel.IsVisible = true;
                RegPhoneLabel.IsVisible = true;

                UsernameFrame.IsVisible = true;
                PasswordFrame.IsVisible = true;

                RegNameFrame.IsVisible = false;
                RegEmailFrame.IsVisible = false;
                RegPhoneFrame.IsVisible = false;
            }
        }

        private async void CheckFields(List<String> props, string type)
        {
            var submit = true;
            
            foreach (var propName in props)
            {
                var entry = FindByName(propName + "Entry") as Entry;
                var label = FindByName(propName + "Label") as Label;

                if (entry.Text == "")
                {
                    entry.Placeholder = "Це поле обов'язкове!";
                    entry.FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Entry));
                    label.IsVisible = false;
                    submit = false;
                }

                if(propName == "RegPhone")
                {
                    string pattern = @"(^\+\d{12}$)|(^\d{10}$)";
                    if(!Regex.IsMatch(RegPhoneEntry.Text, pattern))
                    {
                        RegPhoneEntry.TextColor = Color.Red;
                        submit = false;
                    }
                }
                else if (propName == "RegEmail")
                {
                    string pattern = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
                    if (!Regex.IsMatch(RegEmailEntry.Text, pattern))
                    {
                        RegEmailEntry.TextColor = Color.Red;
                        submit = false;
                    }
                }
                else if (propName == "Username")
                {
                    string emailPattern = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
                    string phonePattern = @"(^\+\d{12}$)|(^\d{10}$)";

                    string loginType = "";
                    if (Regex.IsMatch(UsernameEntry.Text, emailPattern))
                    {
                        loginType = "email";
                    }
                    else if(Regex.IsMatch(UsernameEntry.Text, phonePattern))
                    {
                        loginType = "phone";
                    }
                }
            }
            
            if (submit)
            {
                if(type == "Authorization")
                {
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }
                if(type == "Registration")
                {
                    var client = new HttpClient();
                    var model = new UsersJSON
                    {
                        initials = RegNameEntry.Text,
                        email = RegEmailEntry.Text,
                        phone = RegPhoneEntry.Text
                    };

                    var content = new StringContent(JsonConvert.SerializeObject(model));

                    var response = await client.PostAsync(@"https://dev.jointly.space/users", content);

                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        InfoLabel.IsVisible = true;
                        InfoLabel.Text = "На вказаний E-mail надіслано посилання для активації вашого облікового запису!";
                        InfoLabel.TextColor = Color.Green;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        InfoLabel.IsVisible = true;
                        InfoLabel.Text = "Помилка! Щось пішло не так!";
                        InfoLabel.TextColor = Color.Red;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        InfoLabel.IsVisible = true;
                        InfoLabel.Text = "Такий користувач вже існує! Спробуйте іншу адресу та телефон";
                        InfoLabel.TextColor = Color.Red;
                    }

                    RegNameEntry.Text = "";
                    RegEmailEntry.Text = "";
                    RegPhoneEntry.Text = "";
                    RegNameLabel.IsVisible = true;
                    RegEmailLabel.IsVisible = true;
                    RegPhoneLabel.IsVisible = true;
                }
            }
            else
            {

            }

            IsSubmitButtonClicked = false;
        }
    }
}