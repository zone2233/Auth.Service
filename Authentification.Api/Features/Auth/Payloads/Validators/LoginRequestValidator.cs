using Application.Features.Auth.Payloads.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        {
            RuleFor(u => u.Username).NotEmpty().EmailAddress();
            RuleFor(p => p.Password).NotEmpty().MinimumLength(8);
        }
    }
}
