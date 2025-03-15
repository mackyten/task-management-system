using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.APPLICATION.Common.Responses
{
    public abstract class Response
    {
        protected Response(string message = null)
        {
            Message = message;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
    }
}