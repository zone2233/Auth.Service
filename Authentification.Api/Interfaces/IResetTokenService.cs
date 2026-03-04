using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IResetTokenService
    {
        Task Insert(string email, CancellationToken cancellationToken);
        Task<bool> VerifyTokenReleaseTime(Guid recoveryToken, CancellationToken cancellationToken);
        Task<string> GetUsernameByToken(Guid token, CancellationToken cancellationToken);
    }
}
