//***************************************************************************************
//Developer: <Ashita Gaur>
//Create Date: <17th May,2020>
//Last Updated Date: <20th May,2020>
//Description:To Implement booking service.
//Task:CRUD with opreation with flight
//***************************************************************************************

using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// Provides methods for all CRUD operations for Bookings table.
    /// </summary>
    public class BookingService
    {
        //field that stores reference of object of DbContext
        private AirlineDBEntities db;


        /// <summary>
        /// Constructor for BookingService that initializes reference of DbContext
        /// </summary>
        public BookingService()
        {
            db = new AirlineDBEntities();
        }


        /// <summary>
        /// Shows All Bookings from database
        /// </summary>
        /// <returns>Data from Booking Table</returns>
        ///GET: api/Bookings
        public List<Booking> GetBookings()
        {
            return db.Bookings.ToList();
        }


        /// <summary>
        /// Shows All Bookings from database
        /// </summary>
        /// <returns>Data from Booking Table</returns>
        // GET: api/Bookings/5
        public Booking GetBookingById(int id)
        {
            Booking booking = db.Bookings.Find(id);
            return booking;
        }


        /// <summary>
        /// Takes booking data from user and matches with availible Flights.
        /// </summary>
        /// <param name="booking"> booking details as bookingobject</param>
        /// <returns>Saves data to database</returns>
        ///POST: api/Bookings
        public void UpdateBooking(Booking booking)
        {
            try
            {
                // Checks If there is destination and source is availible
                Flight flight = db.Flights.Where<Flight>(
                    f => booking.Destination.Equals(f.Destination)
                    && booking.Source.Equals(f.Source)
                    ).First();

                // If data does not matches with flight data then it is null
                if (flight == null)
                {
                    throw new Exception("Invalid Details");
                }

                // If AvailableSeats is less then user requuired seats then it does not stores data 
                if (booking.NoOfSeats > flight.AvailableSeats)
                {
                    throw new Exception("Seats unavailable");
                }


                //Customer customer=db.Customers.Where<Flight>(
                //    f => booking.Destination.Equals(f.Destination)
                //    && booking.Source.Equals(f.Source)
                //    ).First();

                //Update the fields
                booking.FlightId = flight.FlightId;
                booking.CustomerId = 100;
                // Counts Ticket Fare
                booking.TicketFare = booking.NoOfSeats * 2000;


                // Sets Ticket Status
                booking.TicketStatus = "NOTC";

                //Adds booking data to booking table 
                db.Bookings.Add(booking);
                //Save all cahnges made in the context in database
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Internal method for geting Booking object to get Booking by Id
        /// </summary>
        /// <param name="bookingId">BookingID to check</param>
        /// <returns>Returns Booking object</returns>
        internal Booking GetBookingsById(int bookingId)
        {
            return db.Bookings.Where(temp => temp.BookingId == bookingId).FirstOrDefault();
        }
    }
}

