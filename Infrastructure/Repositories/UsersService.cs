using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsersService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory) : IUsersService
    {
        public async Task AddLastLoginAttempt(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
                user.LastLoginAttempt = DateTime.Now;
                user.PasswordTrys = 0;
                await context.SaveChangesAsync();
            }
        }

        public async Task<string> GetTOTPKeyByUsername(string username, CancellationToken cancellationToken)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
                return user.TOTPKey;
            }
        }

        public async Task<bool> CheckUsername(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                return await context.Users.AnyAsync(u => EF.Functions.Like(u.Username, username));
                
            }
        }

        public async Task<string> GetPasswordByUsername(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username  == username).FirstOrDefaultAsync();
                return user.Password;
            }
        }

        public async Task<string> GetTOTPSecretKey(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
                return user.TOTPKey;
            }
        }

        public async Task<Users> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                return await context.Users.Where(u => u.UserId == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Users> GetUserByUsername(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                return await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
            }
        }

        public async Task IncrementPasswordTrys(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
                user.PasswordTrys ++;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task InsertTOTPSecret(string username, string key, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).SingleOrDefaultAsync(cancellationToken);
                user.TOTPKey = key;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<Users> InsertUser(Users user, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken)) {
                await context.Users.AddAsync(user, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return user;
            }
        }

        public async Task<int> ReturnPasswordTrys(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync(cancellationToken);
                return user.PasswordTrys;
            }
        }

        public async Task UpdateLastLogin(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
                user.LastLogin = DateTime.Now;
                await context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<bool> VerifyUserByUsername(string username, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
                if (user == null) 
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public async Task<string> GetUsernameByUserId(Guid userId, CancellationToken cancellationToken = default)
        {
            using (UsersContext context = await contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Users user = await context.Users.Where(u => u.UserId ==  userId).FirstOrDefaultAsync();
                return user.Username;
            }
        }
    }
}
