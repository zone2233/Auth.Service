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
    public class VerifyRecoveryTokenRequestValidator : AbstractValidator<VerifyRecoveryTokenRequest>
    {
        public VerifyRecoveryTokenRequestValidator(IPasswordService _passwordService)
        {
            RuleFor(p => p.NewPassword).NotEmpty().Matches(_passwordService.passRegex);

            RuleFor(u => u.RecoveryToken).NotEmpty();
        }
    }
}
