using Jointly.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Jointly.Extensions
{
    public static class ValidateExtension
    {
        public static bool Validate(this string obj, string regex = "")
        {
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            return Regex.IsMatch(obj, regex);
        }
    }
}
