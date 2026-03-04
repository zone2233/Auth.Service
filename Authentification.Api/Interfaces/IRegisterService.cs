using Application.Features.Auth.Payloads.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRegisterService
    {
        Task<Guid?> Register(RegisterRequest registerRequest, CancellationToken cancellationToken);
    }
}
