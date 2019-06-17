using System;
using System.Collections.Generic;
using System.Text;

namespace BadBroker.BusinessLogic.Exceptions
{
    public class HttpServiceException : Exception
    {
        public HttpServiceException() { }
        public HttpServiceException(string message) { }
        public HttpServiceException(string message, Exception inner) { }
    }
}
