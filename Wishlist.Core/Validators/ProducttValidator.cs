using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Models;

namespace Wishlist.Core.Validators
{
    public class ProducttValidator : AbstractValidator<Product>
    {
        public ProducttValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()                
                .SetValidator(new TitleValidator())                
                .WithMessage("Titúlo do produto não pode ser branco!");

            RuleFor(x => x.Price)
             .NotNull()             
             .WithMessage("O preço produto não pode ser vazio!");          


        }
    }
}
