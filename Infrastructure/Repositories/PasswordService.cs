using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    
    public class PasswordService : IPasswordService
    {
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> _contextFactory;
        public PasswordService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public string passRegex { get { return @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':\\""|,.<>\/?]).{8,}$"; } }
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task ModifyPasswordByUsername(string username, string newPassword, CancellationToken cancellationToken, string? oldPassword = null)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = context.Users.Where(u => u.Username == username).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User not found!");
                }
                else
                {
                    if (string.IsNullOrEmpty(oldPassword) == false)
                    {
                        bool verifyPass = VerifyPassword(user.Password, oldPassword);
                        if (!verifyPass)
                        {
                            throw new Exception("Incorrect old password!");
                        }
                    }
                    user.Password = HashPassword(newPassword);
                    user.LastPasswordChange = DateTime.Now;
                    await context.SaveChangesAsync(cancellationToken);
                }
            }
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<double?> VerifyPasswordTrysTime(DateTime nowTime, string username, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = context.Users.Where(u => u.Username == username).FirstOrDefault();
                if (!user.LastLoginAttempt.HasValue)
                {
                    return null;
                }
                double timeLeft = (nowTime - user.LastLoginAttempt.Value).TotalMinutes;
                return timeLeft;
            }
        }
    }
}
