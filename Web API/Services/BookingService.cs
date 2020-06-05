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
using System.Web.Http.Description;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Airline_Reservation.web.CustomExceptions;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// class  implements IBookingsService interface And manages CRUD operations on the Bookings Table
    /// </summary>
    public class BookingsService : IBookingService

    {
        /// <summary>
        /// Method to get list of all bookings
        /// </summary>
        /// <returns></returns>
        public List<Booking> GetALLBookings()
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Bookings.ToList();
            }
        }

        ///// <summary>
        ///// Method fetches the Booking corresponding to BookingId
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public Booking GetBookingById(int id)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                Booking booking = db.Bookings.Find(id);
                return booking;
            }
        }

        ///// <summary>
        ///// Method updates the details of existing booking details 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="booking"></param>
        ///// <returns></returns>
        public bool UpdateBooking(int id, Booking booking)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                //db.Configuration.LazyLoadingEnabled = false;
                //db.Entry(booking).State = EntityState.Modified;

                try
                {

                    //use LINQ query to find the Booking with  Bookingid
                    Booking book = db.Bookings.Where(b => b.BookingId == id).FirstOrDefault();

                    if (book != null)
                    {
                        //update  Booking  details 
                        book.BookingId = booking.BookingId;
                        book.Class = booking.Class;
                        book.DateOfBooking = booking.DateOfBooking;
                        book.DateOfJourney = booking.DateOfJourney;
                        book.Destination = booking.Destination;
                        book.NoOfSeats = booking.NoOfSeats;
                        book.Source = booking.Source;
                        book.TicketFare = booking.TicketFare;
                        book.TicketStatus = booking.TicketStatus;
                        //save changes to the database
                        db.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new BookingsException("Booking does not exist");
                    }

                }
                catch (BookingsException ex)
                {
                    throw new BookingsException(ex.Message);
                }

            }
        }
        /// <summary>
        /// Checks if booking exists or not in th 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BookingExists(int id)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    return db.Bookings.Count(e => e.BookingId == id) > 0;
                }
            }
            catch (BookingsException)
            {

                throw;
            }
        }

        /// <summary>
        /// Method create the new booking account
        /// </summary>
        /// <returns></returns>
        public int AddBooking(Booking booking)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    // Checks If there is destination and source is availible
                    Flight flight = db.Flights.Where<Flight>(
                        f => booking.Destination.Equals(f.Destination)
                        && booking.Source.Equals(f.Source)
                        ).First();

                    // If data does not matches with flight data then it is null
                    if (flight == null)
                    {
                        throw new BookingsException("Invalid Details");
                    }

                    // If AvailableSeats is less then user requuired seats then it does not stores data 
                    if (booking.NoOfSeats > flight.AvailableSeats)
                    {
                        throw new BookingsException("Seats unavailable");
                    }


                    //Customer customer = db.Customers.Where<Customer>(
                    // c => booking.CustomerId.Equals(c.CustomerId) ).First();

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
                    return booking.BookingId;
                }


             
            }
            catch (Exception ex)
            {
                //throw user defined BookingsException
                throw new BookingsException("Flight not available for this route, booking can not be done for your requirment");

            }
        }

        internal int PostBooking(object booking)
        {
            throw new NotImplementedException();
        }

        internal bool DeleteBookingById(int bookingId)
        {
            throw new NotImplementedException();
        }



        ///// <summary>
        ///// Method delete the existing booking account
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public bool DeleteBookingBYId(int id)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                Booking booking = db.Bookings.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                if (booking == null)
                {
                    return false; ;
                }

                db.Bookings.Remove(booking);
                db.SaveChanges();

                return true;
            }
        }



        public Booking GetBookingsById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBookings(int id, Booking booking)
        {
            throw new NotImplementedException();
        }

        public bool AddBookings(Booking booking)
        {
            throw new NotImplementedException();
        }

        public List<Booking> GetALLBookingss()
        {
            throw new NotImplementedException();
        }
    }
}