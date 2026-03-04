using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Requests
{
    public class TOTPCodeRequest
    {
        public string TOTPCode { get; set; }
    }
}
