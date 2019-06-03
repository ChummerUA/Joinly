using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Jointly.Models
{
    public class SignUpUserModel 
    {
        [JsonProperty(PropertyName = "initials")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        [JsonIgnore]
        public bool IsUsernameValid { get; set; }

        [JsonIgnore]
        public bool IsEmailValid { get; set; }

        [JsonIgnore]
        public bool IsPhoneValid { get; set; }
    }
}
