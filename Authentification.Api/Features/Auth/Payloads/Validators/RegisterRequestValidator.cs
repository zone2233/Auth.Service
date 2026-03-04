using Application.Features.Auth.Payloads.Requests;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator(IUsersService _userservice, IPasswordService _passwordService) {

            RuleFor(u => u.Username).NotEmpty().EmailAddress().MustAsync(async (u, cancellationToken)  =>
            {
                return !await _userservice.CheckUsername(u, cancellationToken);
            }).WithMessage("Invalid Email");

            RuleFor(p => p.Password).NotEmpty().Matches(_passwordService.passRegex);

            RuleFor(n => n.FirstName).Matches(Profiles.nameRegex);
            RuleFor(n => n.LastName).Matches(Profiles.nameRegex);

            RuleFor(p => p.PhoneNumber).Matches(Profiles.phoneRegex);

            RuleFor(a => a.Address).NotEmpty();
        }
    }
}
