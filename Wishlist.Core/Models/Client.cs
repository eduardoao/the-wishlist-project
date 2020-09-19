using FluentValidation;
using System;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class Client : BaseModel<Guid>
    {
        public Name Nome { get; private set; }
        public Email Email { get; private set; }

        public Client()
        {

        }

        public Client(Name nome, Email email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            DateCreate = DateTime.Now;
        }

        public static Client ClientBuilder(Name name,  Email email)
        {
            var client = new Client(name,  email);

            client.IsValid();

            return client;
        }
      

        public override bool IsValid()
        {
            var validator = new ClientValidator();
            var result = validator.Validate(this, rst =>
            {
                rst.ThrowOnFailures();
            });

            return result.IsValid;
        }
    }
}
