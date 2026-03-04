using Application.Features.Auth.Payloads.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;

namespace Application.Interfaces
{
    public interface ISendEmailService
    {
        Task<Response> SendRecoveryEmail(EmailRequest emailRequest);
    }
}
