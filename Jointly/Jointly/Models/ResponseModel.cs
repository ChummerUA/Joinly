using System;
using System.Collections.Generic;
using System.Text;

namespace Jointly.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object ResultObject { get; set; }
    }
}
