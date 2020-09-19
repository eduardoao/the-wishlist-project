using FluentValidation;
using System;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class Product : BaseModel<Guid>
    {
        public Product(Title title, Picture picture, double price)
        {
            Id = Guid.NewGuid();
            Title = title;
            Picture = picture;
            Price = price;
        }

        public Title Title { get; private set; }
        public Picture Picture { get; private set; }
        public double Price { get; private set; }

        public override bool IsValid()
        {
            var validator = new ProducttValidator();
            var result = validator.Validate(this, rst =>
            {
                rst.ThrowOnFailures();
            });

            return result.IsValid;
        }
    }
}
