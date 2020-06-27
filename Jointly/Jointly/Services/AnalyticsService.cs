using Jointly.Interfaces;
using Jointly.Models.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace Jointly.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public void TrackError(Exception ex, Dictionary<string, string> parameters = null, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = -1)
        {
            parameters.Add("StackTrace", ex.StackTrace);
            Console.WriteLine($"--- Error: {ex.Message} ---");
            parameters?.ForEach(x => Console.WriteLine($"\t{x.Key}: {x.Value};"));
        }
    }
}
