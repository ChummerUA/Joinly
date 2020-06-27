using Jointly.Models.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Jointly.Interfaces
{
    public interface IAnalyticsService
    {
        void TrackError(Exception ex, Dictionary<string, string> parameters = null, [CallerMemberName]string callerName = "", [CallerLineNumber]int callerLine = -1);
    }
}
