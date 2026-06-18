using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.types.exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException() : base() { }
        public InternalServerException(string message) : base(message) { }
    }
}
