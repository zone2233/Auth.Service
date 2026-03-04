using Application.Interfaces;
using Azure.Identity;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ResetTokenService : IResetTokenService
    {
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> _contextFactory;
        private readonly IUsersService _usersService;
        public ResetTokenService(Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory, IUsersService usersService)
        {
            _usersService = usersService;
            _contextFactory = contextFactory;
        }

        public async Task<string> GetUsernameByToken(Guid token, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                ResetTokens resetTokens = context.ResetTokens.Where(rt => rt.Token == token).SingleOrDefault();
                Guid userId = resetTokens.UserId;
                string username = await _usersService.GetUsernameByUserId(userId, cancellationToken);
                return username;
            }
        }

        public async Task Insert(string email, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                Guid token = Guid.NewGuid();

                Users user = context.Users.Where(u => u.Username == email).FirstOrDefault();
                Guid userId = user.UserId;
                
                ResetTokens resetTokens = new ResetTokens()
                {
                    ReleaseTime = DateTime.Now,
                    Token = token,
                    UserId = userId
                };
                await context.AddAsync(resetTokens);
                await context.SaveChangesAsync();
            }

        }

        public async Task<bool> VerifyTokenReleaseTime(Guid recoveryToken, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                ResetTokens resetTokensUser = context.ResetTokens.Where(u => u.Token == recoveryToken).FirstOrDefault();
                if (resetTokensUser != null) 
                { 
                    if((DateTime.Now - resetTokensUser.ReleaseTime).TotalMinutes > 30)
                    {
                        throw new Exception("Time expired! Try again!");
                    }else
                    {
                        return true;
                    }
                         
                }else
                {
                    throw new Exception("Token not found!");
                }
            }
        }
    }
}
