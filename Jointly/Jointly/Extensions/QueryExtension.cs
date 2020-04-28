using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Jointly.Extensions
{
    public static class QueryExtension
    {
        public static string ToQuery(this NameValueCollection nvc)
        {
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format(
            "{0}={1}",
            HttpUtility.UrlEncode(key),
            HttpUtility.UrlEncode(value))
                ).ToArray();
            return "?" + string.Join("&", array);
        }

        public static string UrlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }
    }
}
