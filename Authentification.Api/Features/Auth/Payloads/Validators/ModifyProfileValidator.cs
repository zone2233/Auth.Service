using Application.Features.Auth.Payloads.Requests;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class ModifyProfileValidator : AbstractValidator<ModifyProfileRequest>
    {
        public ModifyProfileValidator()
        {
            RuleFor(n => n.FirstName).Matches(Profiles.nameRegex);
            RuleFor(n => n.LastName).Matches(Profiles.nameRegex);

            RuleFor(p => p.PhoneNumber).Matches(Profiles.phoneRegex);

            RuleFor(a => a.Address).NotEmpty();
        }
    }
}
