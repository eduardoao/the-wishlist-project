using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Validators;

namespace Wishlist.Core.Models.ValueObject
{
    public struct Email : IValidation
    {
        public Email(string email)
        {
            EmailAddress = email;
            ValidationErrors = new List<ValidationFailure>();
        }
        public string EmailAddress { get; private set; }
        private IList<ValidationFailure> ValidationErrors { get; set; }
        public IList<ValidationFailure> GetValidationFailures()
        {
            return ValidationErrors;
        }
        public IList<ValidationFailure> GetValidationResults()
        {
            throw new NotImplementedException();
        }
        public bool IsValid()
        {
            var validation = new Validators.EmailValidator();
            var result = validation.Validate(this);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }
        public override string ToString() => EmailAddress;
    }
}
