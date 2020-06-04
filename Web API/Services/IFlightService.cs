//=============================================
//  Developer:	<Anshika Jindal>
//  Create date: <17th May,2020>
//  updated date: <20 may,2020>
//  Description : interface is created
//  Task : CRUD operation with Flight
//=============================================

using Airline_Reservation.web.Models;
using System.Collections.Generic;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// created an interface that implements FlightService Class
    /// </summary>
    public interface IFlightService
    {
        List<Flight> GetAllFlights();
    }
}