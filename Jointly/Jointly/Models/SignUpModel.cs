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
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        string _phone;
        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        string _email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
    }
}
