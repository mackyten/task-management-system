using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMS.APPLICATION.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }

        private BaseResponse(bool success, string message, int statusCode, T? data = default)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Data = data;
        }

        // Renamed from 'Success' to 'CreateSuccess' to avoid conflicts
        public static BaseResponse<T> CreateSuccess(T data, string message = "Success", int statusCode = 200)
        {
            return new BaseResponse<T>(true, message, statusCode, data);
        }

        // Renamed from 'Failure' to 'CreateFailure' for consistency
        public static BaseResponse<T> CreateFailure(string message, int statusCode = 400)
        {
            return new BaseResponse<T>(false, message, statusCode);
        }
    }


}