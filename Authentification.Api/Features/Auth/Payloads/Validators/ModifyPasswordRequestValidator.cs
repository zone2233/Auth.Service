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
    public class ModifyPasswordRequestValidator : AbstractValidator<ModifyPasswordRequest>
    {
        public ModifyPasswordRequestValidator(IPasswordService passwordService, IUsersService usersService)
        {
            RuleFor(u => u.Username).NotEmpty().EmailAddress().MustAsync(async (u, cancellationToken) =>
            {
                return await usersService.CheckUsername(u, cancellationToken);
            }).WithMessage("Invalid Email");

            RuleFor(p => p.NewPassword).NotEmpty().Matches(passwordService.passRegex);

            RuleFor(p => p.OldPassword).NotEmpty().Matches(passwordService.passRegex);
        }
    }
}
