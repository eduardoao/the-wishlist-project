using FluentValidation;
using System;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class WishClient : BaseModel<Guid>
    {
        public Client Client { get; set; }
        public Product Product { get; set; }

        public WishClient()
        {

        }

        public WishClient(Client client, Product product)
        {
            Id = Guid.NewGuid();
            Client = client;
            Product = product;
        }    

       

        public static WishClient WishClientBuilder(Client client, Product product)
        {
            var wishClient = new WishClient(client,  product);
            
            wishClient.IsValid();

            return wishClient;
        }

        public override bool IsValid()
        {
            var validator = new WishClientValidator();
            var result = validator.Validate(this, rst =>
            {
                rst.ThrowOnFailures();
            });

            return result.IsValid;
        }
    }
}
