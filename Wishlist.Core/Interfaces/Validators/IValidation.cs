using FluentValidation.Results;
using System.Collections.Generic;

namespace Wishlist.Core.Interfaces.Validators
{
    public interface IValidation
    {
        bool IsValid();
        IList<ValidationFailure> GetValidationResults();
    }
}
