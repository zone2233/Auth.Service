using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public Guid ErrorId { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public ErrorResponse(string message, Guid errorId) : this(message) 
        {
            ErrorId = errorId;
        }

        
    }
}
