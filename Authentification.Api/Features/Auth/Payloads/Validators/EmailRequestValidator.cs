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
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator(IUsersService _usersService) 
        { 
            RuleFor(u => u.EmailAdress).NotEmpty().EmailAddress().MustAsync(async (u, cancellationToken) =>
            {
                return await _usersService.CheckUsername(u, cancellationToken);
            }).WithMessage("Invalid Email");
        }
    }
}
