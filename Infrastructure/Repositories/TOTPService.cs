using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TOTPService() : ITOTPService
    {
        private string GenerateSecretKey()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        public string GenerateTOTPCode(string secretKey)
        {
            byte[] byteSecretKey = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(byteSecretKey);
            var totpCode = totp.ComputeTotp();
            return totpCode;
        }

        public bool VerifyTOTPCode(string secretKey, string totpCode)
        {
            string testCode = GenerateTOTPCode(secretKey);
            byte[] byteSecretKey = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(byteSecretKey);
            bool valid = totp.VerifyTotp(totpCode, out _);
            return valid;
        }

        public Tuple<string, string> GenerateQRCode(string username) 
        { 
            string secretKey = GenerateSecretKey(); 
            string url = new OtpUri(OtpType.Totp, secretKey, username).ToString();
            return Tuple.Create(secretKey, url);
        }
    }
}
