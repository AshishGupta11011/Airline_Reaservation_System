//=============================================
//  Developer:	<Mamta Chauhan>
//  Create date: <17th May,2020>
//  last updated date:<20th may,2020>
//  Description : Exception file is cretaed
//  Task : CRUD operation with Passenger
//=============================================

using System;
using System.Runtime.Serialization;



namespace Airline_Reservation.web.Services
{
    [Serializable]
    public class PassengerException : Exception
    {
        //passengersException created
        public PassengerException()
        {

        }
        public PassengerException(string message) : base(message)
        {

        }

        public PassengerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PassengerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    }
}