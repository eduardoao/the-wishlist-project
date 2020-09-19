using FluentValidation;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Validators
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                    .WithMessage("O primero nome não pode estar em branco")
                .NotNull()
                    .WithMessage("O primero nome não pode estar em branco")
                .MinimumLength(2)
                    .WithMessage("O primero nome precisa ter no minimo 3 caracteres");

            RuleFor(x => x.LastName)
                .NotEmpty()
                    .WithMessage("O sobrenome não pode estar em branco")
                .NotNull()
                    .WithMessage("O sobrenome não pode estar em branco")
                .MinimumLength(2)
                    .WithMessage("O sobrenome precisa ter no minimo 3 caracteres");
        }
    }
}
