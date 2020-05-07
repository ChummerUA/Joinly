using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Jointly.Models.Responses
{
    public class APIResponse<T> : BaseResponse
    {
        public T Result { get; set; }

        public APIResponse(BaseResponse response) : base(response) { }

        public APIResponse(HttpResponseMessage message) : base(message) { }

        public APIResponse(T result)
        {
            Result = result;
        }

        public APIResponse()
        {

        }
    }
}
