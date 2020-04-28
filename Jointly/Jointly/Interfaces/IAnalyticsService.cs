using Jointly.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Interfaces
{
    public interface IAnalyticsService
    {
        void TrackError(Exception ex, Dictionary<string, string> parameters = null);

        void TrackError(BaseResponse response, Dictionary<string, string> parameters = null);
    }
}
