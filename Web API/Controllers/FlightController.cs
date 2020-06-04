//  Developer:	<Anshika Jindal>
//  Create date: <17th May,2020>
//  updated date: <20 may,2020>
//  Description : FlightController is created
//  Task : CRUD operation with Flight
//=========================================


using Airline_Reservation.web.Models;
using Airline_Reservation.web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Airline_Reservation.web.Controllers
{
    public class FlightController : ApiController
    {
        //declare FlightService type instance variable
        FlightService fs;

        FlightController()
        {
            //instantiate fliService class
            fs = new FlightService();
        }


        /// <summary>
        /// Method GetAllFlight() to get a list of all the Flights
        /// </summary>
        /// <returns>List of flights</returns>
        [HttpGet]
        [Route("api/flight")]
        public List<Flight> GetAllFlights()
        {

            try
            {
                //Call GetAllFlights() to fetch all Flights
                List<Flight> flightList = fs.GetAllFlights();

                //return the response
                return flightList;
            }
            catch (FlightException)
            {
                //rethrow
                throw;
            }

        }


        /// <summary>
        /// Method fetches the Flight corresponding to the passed flightid
        /// </summary>
        /// <param name="flightid">indicates id of flight</param>
        /// <returns>returns Flight type value</returns>
        public Flight GetFlightById(int id)
        {
            //check the validity of the input
            if (ModelState.IsValid)
            {
                try
                {
                    //Call GetAllFlights() to fetch all Flights
                    Flight flt = fs.GetFlightById(id);

                    //return the response
                    return flt;
                }
                catch (FlightException)
                {
                    //rethrow
                    throw;
                }
            }

            else
            {
                //throw user defined exception object 
                throw new FlightException("The entered details to fetch the flights are not valid");
            }
        }


        /// <summary>
        /// AddFlight(Flight flight) adds the flight to the Flights table
        /// </summary>
        /// <param name="flight">object of type flight</param>
        /// <returns>integer value indicating the FlightId of the added foodItem</returns>
        [ResponseType(typeof(Flight))]
        [Route("api/flight")]
        public int PostFlight(Flight flight)
        {
            //check the validity of the input
            if (ModelState.IsValid)
            {
                try
                {

                    //Call AddFlight method to fetch all Flight
                    int FlightId = fs.AddFlight(flight);

                    //return the response
                    return FlightId;
                }
                catch (FlightException)
                {
                    //rethrow
                    throw;
                }
            }

            else
            {
                //throw user defined exception object 
                throw new FlightException("The entered details to fetch the Flight are not valid");
            }
        }

        /// <summary>
        /// Method updates or edits the changes of the passed flight in the flight table
        /// </summary>
        /// <param name="flight">object of type flight</param>
        /// <returns>boolean value</returns>
        public bool UpdateFlight(Flight flight)
        {
            //check the validity of the input
            if (ModelState.IsValid)
            {
                try
                {

                    //Call AddFlight method to fetch all Food Items 
                    bool isUpdated = fs.UpdateFlight(flight);

                    //return the response
                    return isUpdated;
                }
                catch (FlightException)
                {
                    //rethrow
                    throw;
                }
            }

            else
            {
                //throw user defined exception object 
                throw new FlightException("The entered details to fetch the Flight are not valid");
            }
        }

        /// <summary>
        /// Method Deletes the flight with FlightId from table Flights
        /// </summary>
        /// <param name="flightid">indicates id of flight</param>
        /// <returns>boolean value</returns>
        public bool DeleteFlightById(int FlightId)
        {
            try
            {

                //Call GetAlllight() to fetch all Flight
                bool isDeleted = fs.DeleteFlightById(FlightId);

                //return the response
                return isDeleted;
            }
            catch (FlightException)
            {
                //rethrow
                throw;
            }

        }
    }
}


    

