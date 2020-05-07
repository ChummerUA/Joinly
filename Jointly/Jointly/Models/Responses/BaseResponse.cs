using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Jointly.Models.Responses
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public HttpStatusCode? StatusCode { get; set; }

        public BaseResponse(BaseResponse response)
        {
            IsSuccess = response.IsSuccess;
            Message = response.Message;
            StatusCode = response.StatusCode;
        }

        public BaseResponse(HttpResponseMessage message)
        {
            IsSuccess = message.IsSuccessStatusCode;
            StatusCode = message.StatusCode;
        }

        public BaseResponse()
        {

        }
    }
}
