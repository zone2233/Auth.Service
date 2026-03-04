using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using OtpNet;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LoginService : ILoginService
    {
        private readonly IJWTService _jwtService;
        private readonly IUsersService _userService;
        private readonly IPasswordService _passwordService;
        private readonly Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> _contextFactory;

        public LoginService(IUsersService usersService, IPasswordService passwordService, IJWTService jwtService, 
            Microsoft.EntityFrameworkCore.IDbContextFactory<UsersContext> contextFactory)
        {
            _userService = usersService;
            _passwordService = passwordService;
            _contextFactory = contextFactory;
            _jwtService = jwtService;
        }
        public async Task<BaseResponse<string>> Login(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            using (UsersContext context = await _contextFactory.CreateDbContextAsync(cancellationToken))
            {
                using (IDbContextTransaction UsersTransaction = await context.Database.BeginTransactionAsync(cancellationToken))
                {
                    try
                    {
                        Users user = context.Users.Where(u => u.Username == loginRequest.Username).FirstOrDefault();
                        if (user == null) 
                        {
                            throw new CustomException("Invalid username or password");
                        }
                        double? timeLeft = await _passwordService.VerifyPasswordTrysTime(DateTime.Now, loginRequest.Username, cancellationToken);
                        if (timeLeft.HasValue && timeLeft < 10)
                        {
                            throw new CustomException($"Time to next login attempt is {(int)(10 - timeLeft)} minutes");
                        }
                        else
                        {
                            if (_passwordService.VerifyPassword(await _userService.GetPasswordByUsername(loginRequest.Username, cancellationToken), loginRequest.Password) == true)
                            {
                                await _userService.UpdateLastLogin(loginRequest.Username, cancellationToken);
                                user.LastLoginAttempt = null;
                                user.PasswordTrys = 0;
                                await context.SaveChangesAsync();
                                await UsersTransaction.CommitAsync(cancellationToken);

                                return new BaseResponse<string>()
                                {
                                    Success = true,
                                    Message = "Success",
                                    Data =
                                    await _jwtService.GenerateAuthJWTToken(loginRequest.Username, string.IsNullOrEmpty(user.TOTPKey)
                                    ? Domain.Enums.AuthentificationState.QrAndSecretKey : Domain.Enums.AuthentificationState.TOTPCode)
                                };

                            }
                            else
                            {
                                int passwordTrys = await _userService.ReturnPasswordTrys(loginRequest.Username);
                                if (passwordTrys < 5)
                                {
                                    await _userService.IncrementPasswordTrys(loginRequest.Username, cancellationToken);
                                    throw new CustomException($"Wrong password! Please try again!\nYou have {5 - passwordTrys} attempts left!");
                                }
                                await _userService.AddLastLoginAttempt(loginRequest.Username, cancellationToken);
                                throw new CustomException("Too many wrong attempts!\nRetry in 10 mins!");
                            }
                        }

                    }catch(Exception ex)
                    {
                        await UsersTransaction.RollbackAsync(cancellationToken);
                        throw ex;
                    }
                }
            }
        }
    }
}
