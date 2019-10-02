using Jointly.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jointly.Extensions
{
    public static class ValidateExtension
    {
        public static async Task<bool> Validate(this string obj, string message, string regex = "")
        {
            var popup = ServiceResolver.Get<IPopupService>();
            if (string.IsNullOrEmpty(obj))
            {
                await popup.ShowAlert(Localization.Localization.Error, message);
                return false;
            }
            return true;
        }
    }
}
