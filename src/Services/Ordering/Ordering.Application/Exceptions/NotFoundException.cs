using System;

namespace Ordering.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}

// The constructor uses the base keyword to invoke the constructor of the ApplicationException class
