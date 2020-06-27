using Jointly.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Models.Responses
{
    public class RefreshTokenObject
    {
        public string Token { get; set; }

        public User User { get; set; }

        public DateTime Expired { get; set; }
    }
}
