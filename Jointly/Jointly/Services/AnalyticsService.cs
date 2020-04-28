using Jointly.Interfaces;
using Jointly.Models.Responses;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace Jointly.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        public void TrackError(Exception ex, Dictionary<string, string> parameters = null)
        {
            parameters.Add("StackTrace", ex.StackTrace);
            Console.WriteLine($"--- Error: {ex.Message} ---");
            parameters?.ForEach(x => Console.WriteLine($"\t{x.Key}: {x.Value};"));
        }

        public void TrackError(BaseResponse response, Dictionary<string, string> parameters = null)
        {
            var _parameters = new Dictionary<string, string>
            {
                { "Message", response?.Message },
            };
            parameters?.ForEach(x => _parameters.Add(x.Key, x.Value));
        }
    }

    [Serializable]
    internal class ResponseExcetption : Exception
    {
        public ResponseExcetption()
        {
        }

        public ResponseExcetption(string message) : base(message)
        {
        }

        public ResponseExcetption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ResponseExcetption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
