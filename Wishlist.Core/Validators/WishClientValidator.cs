using FluentValidation;
using Wishlist.Core.Models;

namespace Wishlist.Core.Validators
{
    public class WishClientValidator : AbstractValidator<WishClient>
    {
        public WishClientValidator()
        {
            RuleFor(x => x.Client)
                .NotNull()
                .SetValidator(new ClientValidator())
                .WithMessage("Cliente é obrigatório!");


            RuleFor(x => x.Product)
             .NotNull()
             .SetValidator(new ProducttValidator())
             .WithMessage("Produto é obrigatório!");


        }
    }
}
