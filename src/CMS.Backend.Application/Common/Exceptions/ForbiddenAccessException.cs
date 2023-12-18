using System;

namespace BusinessFile.Backend.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
