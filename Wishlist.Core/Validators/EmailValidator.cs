using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("Endereço de e-mail inválido.");
        }
    }
}
