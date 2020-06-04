using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline_Reservation.web.Interface
{
    interface IFlightService
    {
        List<Flight> GetAllFlights();
    }
}
