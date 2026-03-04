using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        Task<Users> InsertUser(Users user, CancellationToken cancellationToken = default);
        Task<bool> CheckUsername(string username, CancellationToken cancellationToken = default);
        Task<string> GetPasswordByUsername(string username, CancellationToken cancellationToken = default);
        Task UpdateLastLogin(string username, CancellationToken cancellationToken = default);
        Task<Users> GetUserByUsername(string username, CancellationToken cancellationToken = default);
        Task<Users> GetUserById(Guid id, CancellationToken cancellationToken);
        Task IncrementPasswordTrys(string username, CancellationToken cancellationToken = default);
        Task<int> ReturnPasswordTrys(string username, CancellationToken cancellationToken = default);
        Task AddLastLoginAttempt(string username, CancellationToken cancellationToken = default);
        Task<string> GetTOTPSecretKey(string username, CancellationToken cancellationToken = default);
        Task InsertTOTPSecret(string username, string key, CancellationToken cancellationToken = default);
        Task<bool> VerifyUserByUsername(string username, CancellationToken cancellationToken = default);
        public Task<string> GetTOTPKeyByUsername(string username, CancellationToken cancellationToken);
        public Task<string> GetUsernameByUserId(Guid userId, CancellationToken cancellationToken = default);
    }
}
