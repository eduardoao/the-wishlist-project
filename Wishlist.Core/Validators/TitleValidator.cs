using FluentValidation;
using Wishlist.Core.Models.ValueObject;

namespace Wishlist.Core.Validators
{
    public class TitleValidator : AbstractValidator<Title>
    {
        public TitleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                    .WithMessage("O título do produto não pode estar em branco")
                .NotNull()
                    .WithMessage("O título do produto não pode estar em branco")
                .MinimumLength(2)
                    .WithMessage("O título do produto precisa ter no minimo 3 caracteres")
                .MaximumLength(150)
                 .WithMessage("O título do produto não pode ter mais do que 150 caracteres");
        }
    }
}
