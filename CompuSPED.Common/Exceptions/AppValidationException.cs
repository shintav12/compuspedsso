using System;

namespace CompuSPED.Common.Exceptions
{
    public class AppValidationException : Exception
    {
        public AppValidationException(string message) : base(message) { }
    }
}
