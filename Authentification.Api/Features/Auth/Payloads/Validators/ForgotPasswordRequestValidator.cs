using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator(IUsersService _userservice, IPasswordService passwordService) 
        {
            RuleFor(u => u.Username).NotEmpty().EmailAddress().MustAsync(async (u, cancellationToken) =>
            {
                return await _userservice.CheckUsername(u, cancellationToken);
            }).WithMessage("Invalid Email");

            RuleFor(u => u.TOTPCode).NotEmpty().Length(6, 6);

            RuleFor(p => p.NewPassword).NotEmpty().Matches(passwordService.passRegex);
        }
    }
}
