﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace BadBroker.BusinessLogic.Exceptions
{
    [Serializable]
    public class TradeServiceException : Exception
    {
        private readonly string _resourceName;
        private readonly IList<string> _validationErrors;
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
        protected TradeServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            _resourceName = info.GetString("ResourceName");
            _validationErrors = (IList<string>)info.GetValue("ValidationErrors", typeof(IList<string>));
        }

        public string ResourceName => _resourceName;

        public IList<string> ValidationErrors => _validationErrors;

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ResourceName", ResourceName);

            info.AddValue("ValidationErrors", ValidationErrors, typeof(IList<string>));

            base.GetObjectData(info, context);
        }
    }
}
