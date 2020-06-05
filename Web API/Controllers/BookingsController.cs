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
    /// Controller for Booking data table that handles HTTP requests
    /// </summary>
    public class BookingsController : ApiController
    {
        //declaration of BookingsService type instance variable
        BookingsService bs;

        /// <summary>
        /// Constructor for BookingsController
        /// </summary>
        public BookingsController()
        {
            //declare BookingsService type instance variable
            bs = new BookingsService();
        }

        /// <summary>
        /// Shows All Bookings from database
        /// </summary>
        /// <returns>Data from Booking Table</returns>
        /// GET: api/Bookings
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

        /// <summary>
        /// Get All bookings data from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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
                //throw user defined exception that entries are not in required format
                throw new BookingsException("The entered details are not in required format");
            }
        }


        /// <summary>
        /// Update the booking data on existing booking id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
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
        ///  User enterd data for booking requirment and matches with availible Flights .
        ///  If only flight found for avalible route then it done booking .
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

                    //Call AddBooking method to fetch all Flight
                    int BookingId = bs.AddBooking(booking);

                    //return the response
                    return BookingId;
                }
                catch (BookingsException)
                {
                    //throw 
                    throw;
                }
            }
            else
            {
                //throw user defined exception
                throw new BookingsException("Flight not available for this route, booking can not be done for your requirment");
            }

        }

        /// <summary>
        /// Deletes booking by Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// DELETE: api/Bookings/5
        [ResponseType(typeof(Booking))]
        public bool DeleteBookingById(int id)
        {
            try
            {

                //Checks the booking Id and deletws data of it 
                bool isDeleted = bs.DeleteBookingById(id);

                //return the response
                return isDeleted;
            }
            catch (BookingsException)
            {
                   //rethrow
                throw;
            }
        }

    }

}