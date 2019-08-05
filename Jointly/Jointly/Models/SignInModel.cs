using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jointly.Models
{
    public class SignInModel : BindableBase
    {
        #region variables
        string login;
        string password;
        #endregion

        #region Properties
        [JsonProperty(PropertyName = "phone")]
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        [JsonProperty(PropertyName = "code")]
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        #endregion
    }
}
