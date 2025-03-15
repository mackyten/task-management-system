using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.APPLICATION.Common.Responses
{
    public class SuccessResponse : Response
    {
        public SuccessResponse(string message = "Success") : base(message)
        {
            Success = true;
            StatusCode = 200;
        }
    }

    public class SuccessResponse<T> : Response
    {
        public SuccessResponse(T data, string message = "Success")
        {
            Data = data;
            Message = message;
            Success = true;
            StatusCode = 200;
        }

        public T Data { get; set; }
    }
}