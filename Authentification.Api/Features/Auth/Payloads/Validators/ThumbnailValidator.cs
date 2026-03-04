
using Application.Features.Auth.Payloads.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Payloads.Validators
{
    public class ThumbnailValidator : AbstractValidator<ThumbnailRequest>
    {
        public ThumbnailValidator() 
        { 
            RuleFor(p => p.ThumbnailPath).NotEmpty();
        }
    }
}
