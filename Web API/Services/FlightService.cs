//=============================================
//  Developer:	<Anshika Jindal>
//  Create date: <17th May,2020>
//  updated date: <20 may,2020>
//  Description : FlightService is created
//  Task : CRUD operation with Flight
//=============================================


using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airline_Reservation.web.Services


{
    /// <summary>
    /// class FlightService implements IFlightService interface And manages CRUD operations on the Flights table
    /// </summary>
    public class FlightService : IFlightService
    {
        /// <summary>
        /// Method GetAllFlights() to get a list of all the Flight
        /// </summary>
        /// <returns>List of Flights</returns>
        public List<Flight> GetAllFlights()
        {
            //instantiating AirlineDBEntities Context class
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    //use LINQ query to fetch list of Flights from table flights
                    List<Flight> flights = db.Flights.ToList();

                    //assigning lazy loading to be false
                    db.Configuration.LazyLoadingEnabled = false;

                    //return obtained data 
                    return flights;

                }
            }
            catch (FlightException ex)
            {
                //throw user defined FlightException
                throw new FlightException(ex.Message);
            }

        }

        /// <summary>
        /// AddFlight(Flight flight) adds the flight to the Flights table
        /// </summary>
        /// <param name="flight">object of type Flight</param>
        /// <returns>integer value indicating the Id of the added flight</returns>
        public int AddFlight(Flight flight)
        {
            try
            {
                //instantiating Online_Food_Ordering_SystemEntities3 Context class
                using (AirlineDBEntities db = new AirlineDBEntities())
                {

                    //check if the foodItem already exists
                    Flight flt = db.Flights.Where(f => f.FlightName.Equals(flight.FlightName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                    if (flt != null)
                    {
                        //if exists then throw exception
                        throw new FlightException("Flight already there");

                    }

                    //use LINQ query to Add flight from table flights
                    db.Flights.Add(flight);

                    //save changes to the database
                    db.SaveChanges();

                    Flight flts = db.Flights.Where(f => f.FlightId == flight.FlightId).FirstOrDefault();

                    return flts.FlightId;

                }
            }
            catch (FlightException ex)
            {
                //throw user defined FlightException
                throw new FlightException(ex.Message);
            }

        }


        /// <summary>
        /// Method fetches the Flight corresponding to the passed flightId
        /// </summary>
        /// <param name="flightid">indicates id of flight</param>
        /// <returns>returns flight type value</returns>
        public Flight GetFlightById(int flightid)
        {
            try
            {
                //instantiating AirLineDBEntities Context class
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    //LINQ query to find flight corresponding to passed flightid
                    Flight flt = db.Flights.Where(f => f.FlightId == flightid).FirstOrDefault();

                    //return response
                    return flt;

                }
            }
            catch (FlightException ex)
            {
                //throw our user defined FlightException
                throw new FlightException(ex.Message);
            }
        }



        /// <summary>
        /// Method Deletes the Flight with flightid from table flights
        /// </summary>
        /// <param name="flightid">integer indicating id of Flight</param>
        /// <returns>boolean value indicating whether flight is deleted or not</returns>
        public bool DeleteFlightById(int flightid)
        {
            try
            {
                //instantiating AirLineDBEntities Context class
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    //use LINQ query to find the Flight with id flightid
                    Flight flt = db.Flights.Where(f => f.FlightId == flightid).FirstOrDefault();

                    if (flt != null)
                    //use LINQ query to Add Flight from table flight
                    {
                        //remove item from flight
                        db.Flights.Remove(flt);

                        //save changes to the database
                        db.SaveChanges();

                        return true;
                    }

                    else
                    {
                        throw new FlightException("flight does not exist");
                    }

                }
            }
            catch (FlightException ex)
            {
                //throw user defined FlightException
                throw new FlightException(ex.Message);
            }
        }


        /// <summary>
        /// Method updates or edits the changes of the passed flight in the flights table
        /// </summary>
        /// <param name="flight">object of type flight </param>
        /// <returns>boolean value indicating whether flight is updated or not</returns>
        public bool UpdateFlight(Flight flight)
        {

            try
            {
                //instantiating AirLineDBEntities Context class
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    //use LINQ query to find the Flight with  flightid
                    Flight flt = db.Flights.Where(f => f.FlightId == f.FlightId).FirstOrDefault();

                    if (flt != null)
                    {
                        //update  flight  details 
                        flt.FlightName = flight.FlightName;
                        flt.Source = flight.Source;
                        flt.Destination = flight.Destination;
                        flt.DepartureTime = flight.DepartureTime;
                        flt.ArrivalTime = flight.ArrivalTime;
                        flt.BaggageLimit = flight.BaggageLimit;
                        flt.AvailableSeats = flight.AvailableSeats;
                        //save changes to the database
                        db.SaveChanges();

                        return true;
                    }


                    else
                    {
                        throw new FlightException("Flight does not exist");
                    }

                }

            }
            catch (FlightException ex)
            {
                //throw user defined FlightException
                throw new FlightException(ex.Message);
            }

        }



    }
}