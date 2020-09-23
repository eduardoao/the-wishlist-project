using FluentValidation.Results;
using System;
using System.Collections.Generic;
using Wishlist.Core.Interfaces.Validators;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models.ValueObject
{
    public struct Name : IValidation
    {
        public Name(string firsName, string lastName)
        {
            FirstName = firsName;
            LastName = lastName;
            ValidationErrors = new List<ValidationFailure>();
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

       
               
        private IList<ValidationFailure> ValidationErrors { get; set; }
     

        public IList<ValidationFailure> GetValidationResults()
        {
            return ValidationErrors;
        }

        public bool IsValid()
        {
            var validation = new NameValidator();
            var result = validation.Validate(this);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }
        public override string ToString() => $"{FirstName} {LastName}";
    }
}
