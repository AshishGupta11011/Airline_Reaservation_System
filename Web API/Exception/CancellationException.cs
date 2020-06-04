using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Airline_Reservation.web.Exception
{
    public class CancellationException : ApplicationException
    {
        public CancellationException()
        {
        }

        public CancellationException(string message) : base(message)
        {
        }

        public CancellationException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected CancellationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}