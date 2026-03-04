using Application.Features.Auth.Payloads.Requests;
using Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILoginService
    {
        Task<BaseResponse<string>> Login(LoginRequest loginRequest, CancellationToken cancellationToken);
    }
}
