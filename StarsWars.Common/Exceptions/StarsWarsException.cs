using System;
using System.Runtime.Serialization;

namespace StarsWars.Common.Exceptions
{
    [Serializable]
    public class StarsWarsException : Exception
    {
        public StarsWarsException()
        {
        }

        public StarsWarsException(string message) : base(message)
        {
        }

        public StarsWarsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StarsWarsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public int Code { get; set; }
    }
}