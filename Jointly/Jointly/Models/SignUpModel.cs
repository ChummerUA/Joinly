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
        string _email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        string _phone;
        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
    }
}
