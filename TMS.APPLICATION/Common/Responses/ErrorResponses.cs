using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.APPLICATION.Common.Responses
{
    public class NotFoundResponse : Response
    {
        public NotFoundResponse(string message = "Resource not found") : base(message)
        {
            Success = false;
            StatusCode = 404;
        }
    }

    public class BadRequestResponse : Response
    {
        public BadRequestResponse(string message = "Bad request") : base(message)
        {
            Success = false;
            StatusCode = 400;
        }
    }

    public class InvalidParameterResponse : Response
    {
        public InvalidParameterResponse(string message = "Invalid parameters") : base(message)
        {
            Success = false;
            StatusCode = 422;
        }
    }
}