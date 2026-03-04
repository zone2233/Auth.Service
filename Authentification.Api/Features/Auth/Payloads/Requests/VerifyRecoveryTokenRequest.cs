using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Requests
{
    public class VerifyRecoveryTokenRequest
    {
        public Guid RecoveryToken { get; set; }
        public string NewPassword { get; set; }
    }
}
