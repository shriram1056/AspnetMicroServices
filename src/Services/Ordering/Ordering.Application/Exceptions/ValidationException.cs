using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());

            // The GroupBy method is called on failures, grouping the error objects based on the PropertyName property. This means errors with the same PropertyName will be grouped together. e => e.ErrorMessage specifies the element selector, indicating that we want to select the ErrorMessage property of each error object.
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}

// The constructor uses the base keyword to invoke the constructor of the ApplicationException class