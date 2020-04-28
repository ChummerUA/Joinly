using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Resources
{
    public static class Constants
    {
        public const string APIURL = "https://dev.jointly.space";

        #region Navigation

        #endregion

        #region validation
        public const string EmailRegex = @"^([A-Za-z0-9_\.\-]+\@[A-Za-z]+\.[A-Za-z\.]+)$";
        public const string PhoneRegex = @"(^\+\d{12}$)|(^\d{10}$)";

        #endregion

    }
}
