//=============================================
//  Developer:	<Anshika Jindal>
//  Create date: <17th May,2020>
//  last updated date:<20th may,2020>
//  Description : Exception file is cretaed
//  Task : CRUD operation with Flight
//=============================================

using System;
using System.Runtime.Serialization;

namespace Airline_Reservation.web.Services
{
    [Serializable]
    //FlightException is created 
    internal class FlightException : ApplicationException
    {
        public FlightException()
        {
        }

        public FlightException(string message) : base(message)
        {
        }

        public FlightException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected FlightException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}