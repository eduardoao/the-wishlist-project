using FluentValidation.Results;
using System.Collections.Generic;
using Wishlist.Core.Interfaces.Validators;
using Wishlist.Core.Validators;

namespace Wishlist.Core.Models.ValueObject
{
    public struct Title : IValidation
    {
        public Title(string name)
        {
            Name = name;           
            ValidationErrors = new List<ValidationFailure>();
        }
        public string Name { get; private set; }       
        private IList<ValidationFailure> ValidationErrors { get; set; }
     

        public IList<ValidationFailure> GetValidationResults()
        {
            return ValidationErrors;
        }

        public bool IsValid()
        {
            var validation = new TitleValidator();
            var result = validation.Validate(this);
            ValidationErrors = result.Errors;
            return result.IsValid;
        }
        public override string ToString() => $"{Name}";
    }
}
