using System;
using System.Runtime.Serialization;

namespace ProjectForTesting.Helpers
{
    [Serializable]
    public class BadRequestError : Exception
    {
        public BadRequestError(string message) : base(message) { }

        public BadRequestError(string message, int id) : base($"{message}: {id}") { }

        public BadRequestError() { }

        public BadRequestError(string message, Exception inner) : base(message, inner) { }
    }
}
