using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Models;

namespace Wishlist.Core.Validators
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .SetValidator(new EmailValidator())
                .WithMessage("O email não pode estar em branco");
        }
    }
}
