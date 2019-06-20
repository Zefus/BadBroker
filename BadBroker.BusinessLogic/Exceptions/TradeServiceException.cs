using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Exceptions
{
    [Serializable()]
    public class TradeServiceException : Exception
    {
        /// <summary>
        /// Just create the exception
        /// </summary>
        public TradeServiceException() : base() { }
        /// <summary>
        /// Create the exception with description
        /// </summary>
        /// <param name="message">Exception description</param>
        public TradeServiceException(string message) : base(message) { }
        /// <summary>
        /// Create the exception with description and inner cause
        /// </summary>
        /// <param name="message">Exception description</param>
        /// <param name="inner">Exception inner cause</param>
        public TradeServiceException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// Create the exception from serialized data.
        /// Usual scenario is when exception is occured somewhere on the remote workstation
        /// and we have to re-create/re-throw the exception on the local machine
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Serialization context</param>
        public TradeServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
