//***************************************************************************************
//Developer: <Ashita Gaur>
//Create Date: <17th May,2020>
//Last Updated Date: <20th May,2020>
//Description:TO Create booking service interface
//Task:CRUD with opreation with booking
//***************************************************************************************



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airline_Reservation.web.Models;


namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// interface for BookingssService 
    /// </summary>
    interface IBookingService
    {
        /// <summary>
        /// Method to get details of all bookings
        /// </summary>
        /// <returns> List<Bookings></returns>
        List<Booking> GetALLBookingss();

        /// <summary>
        /// method to get detail of a booking by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Booking GetBookingsById(int id);
        /// <summary>
        /// Method to update Bookingss details 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        bool UpdateBookings(int id, Booking booking);

        /// <summary>
        /// Method to Create bookings account
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        bool AddBookings(Booking booking);

       
    }
}

