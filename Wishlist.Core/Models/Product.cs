using FluentValidation;
using System;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class Product : BaseModel<Guid>
    {
        public Product(Title title, Picture picture, double price, string brand)
        {
            Id = Guid.NewGuid();
            Title = title;
            Picture = picture;
            Price = price;
            DateCreate = DateTime.Now;
            Brand = brand;

        }

        public Product()
        {

        }

        public Title Title { get; private set; }
        public Picture Picture { get; private set; }
        public double Price { get; private set; }
        public string Brand { get; private set; }
        public int ReviewScore { get; set; }


        public int IsEnable { get;private set; }

        public void SetReviewScore(int score)
        {
            ReviewScore = score;
        }



        public override bool IsValid()
        {
            var validator = new ProducttValidator();
            var result = validator.Validate(this, rst =>
            {
                rst.ThrowOnFailures();
            });

            return result.IsValid;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
