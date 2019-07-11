using System;
using System.Runtime.Serialization;

namespace BadBroker.BusinessLogic.Exceptions
{
    [Serializable()]
    public class InvalidDateException : Exception
    {
        /// <summary>
        /// Just create the exception
        /// </summary>
        public InvalidDateException() : base() { }
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public InvalidDateException(string message) : base(message) { }
        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="inner">Exception inner cause</param>
        public InvalidDateException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        public InvalidDateException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
