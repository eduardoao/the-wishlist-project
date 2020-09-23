using FluentValidation;
using System;
using System.Collections.Generic;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class WishClient : BaseModel<Guid>
    {
        public Client Client { get; private set; }
        public Product Product { get; private set; }

        public IEnumerable<Product> ListProducts { get; private set; }

        public WishClient()
        {

        }

        public WishClient(Client client, Product product)
        {
            Id = Guid.NewGuid();
            Client = client;
            Product = product;
        }    

        public IEnumerable<Product>GetProducts()
        {
            return ListProducts;
        }
       

        public static WishClient WishClientBuilder(Client client, Product product)
        {
            var wishClient = new WishClient(client,  product);
            
            //wishClient.IsValid();

            return wishClient;
        }



        public override bool IsValid()
        {
            var validator = new WishClientValidator();

            var result = validator.Validate(this);
            foreach (var item in result.Errors)
            {
                this.Errors.Add(item);
            }
            return result.IsValid;
        }
    }
}
