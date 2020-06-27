using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Jointly.Interfaces;

namespace Jointly.Droid.Services
{
    public class Native_Droid : INative
    {
        private readonly Context Context;
        private string uniqueID;
        private const string PREF_UNIQUE_ID = "UUID";

        public Native_Droid(Context context)
        {
            Context = context;
        }

        public string GetUUID()
        {
            if (string.IsNullOrWhiteSpace(uniqueID))
            {
                var sharedPrefs = Context.GetSharedPreferences(
                   "Jointly", FileCreationMode.Private);
                uniqueID = sharedPrefs.GetString(PREF_UNIQUE_ID, "");
                if (string.IsNullOrWhiteSpace(uniqueID))
                {
                    uniqueID = UUID.RandomUUID().ToString();
                    sharedPrefs
                        .Edit()
                        .PutString(PREF_UNIQUE_ID, uniqueID)
                        .Commit();
                }
            }
            return uniqueID;
        }
    }
}