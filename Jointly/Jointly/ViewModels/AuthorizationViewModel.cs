using GalaSoft.MvvmLight.Command;
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

        private bool IsButtonClicked { get; set; }
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

        #region Commands
        public RelayCommand ChangeAuthTypeCommand { get; }

        //SignUp commands
        public RelayCommand<string> SetUsernameCommand { get; }
        public RelayCommand<string> SetEmailCommand { get; }
        public RelayCommand<string> SetPhoneCommand { get; }

        public RelayCommand SignUpCommand { get; }

        //SignIn commands
        public RelayCommand<string> SetLoginCommand { get; }
        public RelayCommand<string> SetPasswordCommand { get; }

        public RelayCommand SignInCommand { get; }
        #endregion

        public AuthorizationViewModel()
        {
            SignInUserModel = new SignInUserModel
            {
                Login = "",
                Password = "",
                IsSuccess = false
            };

            SetUsernameCommand = new RelayCommand<string>((parameter) => SetLogin(parameter));
            SetPasswordCommand = new RelayCommand<string>((parameter) => SetPassword(parameter));
            SignInCommand = new RelayCommand(execute: SignIn,
                canExecute: CanSignIn);

            SignUpUserModel = new SignUpUserModel
            {
                Username = "",
                Email = "",
                Phone = "",
                IsUsernameValid = false,
                IsEmailValid = false,
                IsPhoneValid = false
            };

            SetUsernameCommand = new RelayCommand<string>(execute: (parameter) => SetUsername(parameter));
            SetEmailCommand = new RelayCommand<string>(execute: (parameter) => SetEmail(parameter));
            SetPhoneCommand = new RelayCommand<string>(execute: (parameter) => SetPhone(parameter));
            SignUpCommand = new RelayCommand(execute: async () => await SignUpAsync(),
                canExecute: CanSignUp);

            ChangeAuthTypeCommand = new RelayCommand(ChangeAuthType, CanChangeAuthType);
            AuthType = "SignIn";
            Message = "";
            MessageColor = Color.Red;
            IsMessageVisible = false;
            IsButtonClicked = false;
            RaiseCanExecuteChanged();
        }

        #region SignUp
        private void SetUsername(string username)
        {
            SignUpUserModel.Username = username;
            if (username != "")
            {
                SignUpUserModel.IsUsernameValid = true;
            }
            else
            {
                SignUpUserModel.IsUsernameValid = false;
            }
        }

        private void SetEmail(string email)
        {
            SignUpUserModel.Email = email;
            string pattern = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
            if(Regex.IsMatch(email, pattern))
            {
                SignUpUserModel.IsEmailValid = true;
            }
            else
            {
                SignUpUserModel.IsEmailValid = false;
            }
        }

        private void SetPhone(string phone)
        {
            SignUpUserModel.Phone = phone;
            string pattern = @"(^\+\d{12}$)|(^\d{10}$)";
            if(Regex.IsMatch(phone, pattern))
            {
                SignUpUserModel.IsPhoneValid = true;
            }
            else
            {
                SignUpUserModel.IsPhoneValid = false;
            }
        }

        private async Task SignUpAsync()
        {
            IsButtonClicked = true;
            RaiseCanExecuteChanged();

            var connectivity = Connectivity.NetworkAccess;

            if(connectivity != NetworkAccess.Internet)
            {
                Message = "Відсутній доступ до інтернету";
                IsButtonClicked = false;
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

            IsButtonClicked = false;
            RaiseCanExecuteChanged();
        }

        private bool CanSignUp()
        {
            if (IsButtonClicked)
            {
                return false;
            }

            if (SignUpUserModel.Username == "" ||
                SignUpUserModel.Email == "" ||
                SignUpUserModel.Phone == "")
            {
                Message = "Заповніть усі поля!";
                return false;
            }
            if (!SignUpUserModel.IsUsernameValid ||
                !SignUpUserModel.IsEmailValid ||
                !SignUpUserModel.IsPhoneValid)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region SignIn
        private void SetLogin(string login)
        {
            SignInUserModel.Login = login;
            SignInCommand.RaiseCanExecuteChanged();
        }

        private void SetPassword(string password)
        {
            SignInUserModel.Password = password;
            SignInCommand.RaiseCanExecuteChanged();
        }

        private void SignIn()
        {
            IsButtonClicked = true;
            RaiseCanExecuteChanged();
        }

        private bool CanSignIn()
        {
            if (IsButtonClicked)
            {
                return false;
            }

            if(SignInUserModel.Login == "" ||
                SignInUserModel.Password == "")
            {
                return false;
            }
            return true;
        }
        #endregion

        private void ChangeAuthType()
        {
            RaiseCanExecuteChanged();
            if (AuthType == "SignIn")
            {
                AuthType = "SignUp";
            }
            else if (AuthType == "SignUp")
            {
                AuthType = "SignIn";
            }
            RaiseCanExecuteChanged();
        }
        
        private bool CanChangeAuthType()
        {
            return !IsButtonClicked;
        }

        private void RaiseCanExecuteChanged()
        {
            SignInCommand.RaiseCanExecuteChanged();
            SignUpCommand.RaiseCanExecuteChanged();
            ChangeAuthTypeCommand.RaiseCanExecuteChanged();
        }
    }
}
