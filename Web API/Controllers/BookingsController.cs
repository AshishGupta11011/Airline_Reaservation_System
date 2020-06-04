//***************************************************************************************
//Developer: <Ashita Gaur>
//Create Date: <17th May,2020>
//Last Updated Date: <20th May,2020>
//Description:To perform Business logic and accordingly return response to Bookings.
//Task:CRUD with opreation with booking
//***************************************************************************************

using Airline_Reservation.web.CustomExceptions;
using Airline_Reservation.web.Models;
using Airline_Reservation.web.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace Airline_Reservation.web.Controllers
{
    /// <summary>
    /// Controller for Bookings table that handles HTTP requests
    /// </summary>
    public class BookingsController : ApiController
    {
        //declare BookingsService type instance variable
        BookingsService bs;

        /// <summary>
        /// Constructor for BookingsController
        /// </summary>
        public BookingsController()
        {
            //declare CustomersService type instance variable
            bs = new BookingsService();
        }

        /// <summary>
        /// Shows All Bookings from database
        /// </summary>
        /// <returns>Data from Booking Table</returns>

        // GET: api/Bookings
        public List<Booking> GetBookings()
        {
            try
            {
                List<Booking> booking = bs.GetALLBookings();
                return booking;
            }
            catch (BookingsException)
            {

                throw;
            }
        }


        // GET: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBooking(int id)
        {
            //check the validity of the input
            if (ModelState.IsValid)
            {
                try
                {
                    Booking booking = bs.GetBookingById(id);
                    if (booking == null)
                    {
                        return NotFound();
                    }
                    return Ok(booking);
                }
                catch (BookingsException)
                {

                    throw;
                }
            }

            else
            {
                //throw user defined exception object 
                throw new BookingsException("The entered details are not valid");
            }
        }


        // PUT: api/Bookings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBooking(int id, Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != booking.BookingId)
            {
                return BadRequest();
            }


            try
            {
                bs.UpdateBooking(id, booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Takes booking data from user and matches with availible Flights.
        /// </summary>
        /// <param name="booking"> booking details as bookingobject</param>
        /// <returns>Saves data to database</returns>

        // POST: api/Bookings
        [ResponseType(typeof(Booking))]
        public int PostBooking(Booking booking)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //Call AddFlight method to fetch all Flight
                    int BookingId = bs.AddBooking(booking);

                    //return the response
                    return BookingId;
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
                throw new BookingsException("The entered details are not valid");
            }

        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public bool DeleteBookingById(int id)
        {
            try
            {

                //Call GetAlllight() to fetch all Flight
                bool isDeleted = bs.DeleteBookingById(id);

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