using Jointly.Models.Responses;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Models
{
    public class User : BindableBase
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }
    }
}
