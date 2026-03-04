using Application.Features.Auth.Payloads.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class ProfilePictureValidator : AbstractValidator<IFormFile>
    {
        public ProfilePictureValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .Must(f => f.Length < 5 * 1024 * 1024 * 100);
            RuleFor(x => x.FileName)
                .NotNull()
                .Must(f => f.EndsWith(".png") || f.EndsWith(".jpg") || f.EndsWith(".jpeg"));
        }
    }
}
