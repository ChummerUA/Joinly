using Newtonsoft.Json;
using Prism.Mvvm;

namespace Jointly.Models.Domain
{
    public class SignInModel : BindableBase
    {
        string login;
        [JsonProperty(PropertyName = "login")]
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        string password;
        [JsonProperty(PropertyName = "code")]
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        [JsonProperty(PropertyName = "uuid")]
        public string UUID { get; set; }
    }
}
