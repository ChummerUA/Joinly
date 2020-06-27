using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string baseStr)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(baseStr);

            return Convert.ToBase64String(toEncodeAsBytes);
        }
    }
}
