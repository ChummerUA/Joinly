using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Resources
{
    public static class Constants
    {
        public const string APIURL = "https://dev.jointly.space/";

        public static class Navigation
        {
            public const string Created = "Created";
        }

        public static class Validation
        {
            public const string EmailRegex = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
            public const string PhoneRegex = @"(^\+\d{12}$)|(^\d{10}$)";
        }

        public static class Preferences
        {
            public const string TokenKey = "Token";
        }
    }
}
