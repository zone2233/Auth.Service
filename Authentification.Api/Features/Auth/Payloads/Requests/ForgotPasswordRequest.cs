using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Requests
{
    public class ForgotPasswordRequest
    {
        public string Username { get; set; }
        public string TOTPCode { get; set; }
        public string NewPassword { get; set; }
    }
}
