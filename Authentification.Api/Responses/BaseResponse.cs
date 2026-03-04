
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public BaseResponse() { }
        public BaseResponse(bool success, string message, T data) 
        { 
            Success = success;
            Message = message; 
            Data = data;
        }
    }
}
