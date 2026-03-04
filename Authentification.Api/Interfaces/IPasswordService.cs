using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPasswordService
    {
        string passRegex {  get; }
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword, string password);
        Task<double?> VerifyPasswordTrysTime(DateTime nowTime, string username, CancellationToken cancellationToken);
        Task ModifyPasswordByUsername(string username ,string newPassword, CancellationToken cancellationToken, string? oldPassword = null);
    }
}
