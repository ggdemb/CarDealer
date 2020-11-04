using System;
using System.Collections.Generic;

namespace Api.Utils
{
    public class Envelope<T>
    {
        public T Result { get; }
        public List<string> ErrorMessages { get; }
        public DateTime TimeGenerated { get; }

        protected internal Envelope(T result, List<string> errorMessages)
        {
            Result = result;
            ErrorMessages = errorMessages;
            TimeGenerated = DateTime.UtcNow;
        }
    }

    public class Envelope : Envelope<string>
    {
        protected Envelope(List<string> errorMessages)
            : base(null, errorMessages)
        {
        }

        public static Envelope<T> Ok<T>(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope Ok()
        {
            return new Envelope(null);
        }

        public static Envelope Error(List<string> errorMessages)
        {
            return new Envelope(errorMessages);
        }
        public static Envelope Error(string errorMessage)
        {
            return new Envelope(new List<string>() { errorMessage });
        }
    }
}
