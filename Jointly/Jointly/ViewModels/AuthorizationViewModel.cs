using Jointly.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jointly.ViewModels
{
    public class AuthorizationViewModel : INotifyPropertyChanged
    {
        #region variables
        SignInUserModel signInUserModel;
        SignUpUserModel signUpUserModel;
        string authType;
        string message;
        Color messageColor;
        bool isMessageVisible;
        #endregion

        #region Properties
        public SignInUserModel SignInUserModel
        {
            get
            {
                return signInUserModel;
            }
            set
            {
                if(value != signInUserModel)
                {
                    signInUserModel = value;
                    OnPropertyChanged("SignInUserModel");
                }
            }
        }

        public SignUpUserModel SignUpUserModel
        {
            get
            {
                return signUpUserModel;
            }
            set
            {
                if(value != signUpUserModel)
                {
                    signUpUserModel = value;
                    OnPropertyChanged("SignUpUserModel");
                }
            }
        }

        public string AuthType
        {
            get
            {
                return authType;
            }
            set
            {
                if(value != authType)
                {
                    authType = value;
                    OnPropertyChanged("AuthType");
                }
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                if(value != message)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public Color MessageColor
        {
            get
            {
                return messageColor;
            }
            set
            {
                if(value != messageColor)
                {
                    messageColor = value;
                    OnPropertyChanged("MessageColor");
                }
            }
        }

        public bool IsMessageVisible
        {
            get
            {
                return isMessageVisible;
            }
            set
            {
                if (value != isMessageVisible)
                {
                    isMessageVisible = value;
                    OnPropertyChanged("IsMessageVisible");
                }
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;

            if(changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region ICommand
        public ICommand SetUsernameCommand { get; }
        public ICommand SetEmailCommand { get; }
        public ICommand SetPhoneCommand { get; }

        public ICommand SignUpCommand { get; }
        #endregion

        public AuthorizationViewModel()
        {
            SignInUserModel = new SignInUserModel
            {
                Login = "",
                Password = ""
            };
            SignUpUserModel = new SignUpUserModel
            {
                Username = "",
                Email = "",
                Phone = ""
            };

            AuthType = "SignIn";
            Message = "";
            MessageColor = Color.Red;
            IsMessageVisible = false;

            SetUsernameCommand = new Command<string>(execute: (parameter) => SetUsername(parameter), 
                canExecute: (parameter) => CheckUsername(parameter));
            SetEmailCommand = new Command<string>(execute: (parameter) => SetEmail(parameter),
                canExecute: (parameter) => CheckEmail(parameter));
            SetPhoneCommand = new Command<string>(execute: (parameter) => SetPhone(parameter),
                canExecute: (parameter) => CheckPhone(parameter));

            SignUpCommand = new Command(execute: async () => await SignUpAsync());
        }

        #region Execute
        private void SetUsername(string username)
        {
            SignUpUserModel.Username = username;
        }

        private void SetEmail(string email)
        {
            SignUpUserModel.Email = email;
        }

        private void SetPhone(string phone)
        {
            SignUpUserModel.Phone = phone;
        }

        private async Task SignUpAsync()
        {
            if (!CanSignUp())
            {
                MessageColor = Color.Red;
                IsMessageVisible = true;
                return;
            }

            var client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(SignUpUserModel));

            var response = await client.PostAsync(@"https://dev.jointly.space/users", content);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Message = "На вказаний E-mail надіслано посилання для активації вашого облікового запису!";
                MessageColor = Color.Green;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Message = "Помилка! Щось пішло не так!";
                MessageColor = Color.Red;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                Message = "Такий користувач вже існує! Спробуйте іншу адресу та телефон";
                MessageColor = Color.Red;
            }
            IsMessageVisible = true;
        }
        #endregion

        #region CanExecute
        private bool CheckUsername(string username)
        {
            if(username == null)
            {
                return false;
            }
            return true;
        }

        private bool CheckEmail(string email)
        {
            if(email == null)
            {
                return false;
            }

            string pattern = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
            if (!Regex.IsMatch(email, pattern))
            {
                return false;
            }

            return true;
        }

        private bool CheckPhone(string phone)
        {
            if (phone == null)
            {
                return false;
            }

            string pattern = @"(^\+\d{12}$)|(^\d{10}$)";
            if (!Regex.IsMatch(phone, pattern))
            {
                return false;
            }
            return true;
        }

        private bool CanSignUp()
        {
            var current = Connectivity.NetworkAccess;

            if(current != NetworkAccess.Internet)
            {
                Message = "Відсутній доступ до інтернету";
                return false;
            }

            if (SignUpUserModel.Username == "" || 
                SignUpUserModel.Email == "" || 
                SignUpUserModel.Phone == "")
            {
                Message = "Потрібно заповнити усі поля";
                return false;
            }
            if (!SetUsernameCommand.CanExecute(SignUpUserModel.Username) ||
                !SetEmailCommand.CanExecute(SignUpUserModel.Email) ||
                !SetPhoneCommand.CanExecute(SignUpUserModel.Phone))
            {
                Message = "Неправильно заповнені поля";
                return false;
            }

            return true;
        }
        #endregion
    }
}
