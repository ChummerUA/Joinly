using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace Jointly.Models
{
    public class SignUpModel : BindableBase
    {
        string username;
        [JsonProperty(PropertyName = "initials")]
        public string Username
        {
            get => username;
            set
            {
                SetProperty(ref username, value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsUsernameValid)));
            }
        }

        string email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEmailValid)));
            }
        }

        string phone;
        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get => phone;
            set
            {
                SetProperty(ref phone, value);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsPhoneValid)));
            }
        }

        [JsonIgnore]
        public bool IsUsernameValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Username);
            }
        }

        [JsonIgnore]
        public bool IsPhoneValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Phone))
                {
                    return false;
                }

                string pattern = @"(^\+\d{12}$)|(^\d{10}$)";
                if (!Regex.IsMatch(Phone, pattern))
                {
                    return false;
                }
                return true;
            }
        }

        [JsonIgnore]
        public bool IsEmailValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Email))
                {
                    return false;
                }

                string pattern = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
                if (!Regex.IsMatch(Email, pattern))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
