using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Validators;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models.ValueObject
{
    public struct Picture : IValidation
    {
        public Picture(string url)
        {
            Url = url;
            ValidationErrors = new List<ValidationFailure>();

        }
        public string Url { get; private set; }
        public string Extension
        {
            get
            {
                return Url.Substring(Url.LastIndexOf('.'));
            }
        }

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
            var validation = new PictureValidator();
            var result = validation.Validate(this);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }
    }
}
