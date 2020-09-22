using FluentValidation;
using System;
using System.Linq;
using Wishlist.Core.Models.ValueObject;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models
{
    public class Client : BaseModel<Guid>
    {
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public bool IsEnable { get; private set; }

        public Client()
        {

        }

        public Client(Guid id)
        {
            Id = id;
        }

        public Client(Name nome, Email email)
        {
            Id = Guid.NewGuid();
            Name = nome;
            Email = email;
            DateCreate = DateTime.Now;
        }

        public static Client ClientBuilder(Name name,  Email email)
        {
            var client = new Client(name,  email);

            client.IsValid();

            return client;
        }

        public static Client ClientBuilder(Guid id)
        {
            var client = new Client(id);          

            return client;
        }


        public override bool IsValid()
        {
            var validator = new ClientValidator();
            
            var result = validator.Validate(this);
            foreach (var item in result.Errors)
            {
                this.Errors.Add(item);
            }  
            return result.IsValid;           
        }

        public void SetIsEnable(bool value)
        {
            this.IsEnable = value;
        }
    }
}
