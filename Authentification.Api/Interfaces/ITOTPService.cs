using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using OtpNet;

namespace Application.Interfaces
{
    public interface ITOTPService
    {
        public string GenerateTOTPCode(string secretKey);
        public bool VerifyTOTPCode(string totpCode, string secretKey);
        public Tuple<string, string> GenerateQRCode(string username);
    }
}
