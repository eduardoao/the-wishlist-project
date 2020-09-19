using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using Wishlist.Core.Interfaces.Validators;

namespace Wishlist.Core.Models
{
    public abstract class BaseModel<T> : IValidation
    {
        public T Id { get; protected set; }
        public abstract bool IsValid();

        public virtual IList<ValidationFailure> GetValidationResults()
        {
            return Errors;
        }

        public IList<ValidationFailure> Errors { get; private set; }
    }
}
