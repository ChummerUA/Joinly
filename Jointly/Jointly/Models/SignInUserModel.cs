using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Jointly.Models
{
    public class SignInUserModel
    {
        [JsonProperty(PropertyName = "phone")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string Password { get; set; }

        [JsonIgnore]
        public bool IsSuccess { get; set; }
    }
}
