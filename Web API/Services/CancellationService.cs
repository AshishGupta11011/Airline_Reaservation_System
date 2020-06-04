//-----------------------------------------------------------------------------------------
// Developer    :  Aniket Anand
// File Name    :  CancellationsController.cs
// Create Date  :  <15th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  To Handles Cancellations request and send response accordingly.
// Task         :  CRUD  opreation with Cancellation table in database
// ------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using Airline_Reservation.web.CustomExceptions;
using Airline_Reservation.web.Models;

namespace Airline_Reservation.web.Services
{
    public class CancellationService
    {
        /// <summary>
        /// Method to get list of all customers
        /// </summary>
        /// <returns>List of cancelled tickets</returns>
        public List<Cancellation> GetALLCancellation()
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Cancellations.ToList();
            }
        }

        ///// <summary>
        ///// Method fetches the Cancelled ticket corresponding to Cancellation Id
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>Cancelled ticket</returns>
        public Cancellation GetCancellationById(int id)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    Cancellation cancellation = db.Cancellations.Find(id);
                    return cancellation;
                }
            }
            catch (CancellationException)
            {

                throw new CancellationException("Id doesn't Exist");
            }
        }

        ///// <summary>
        ///// Method updates the details of existing cancellation details 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="customer"></param>
        ///// <returns>return bool value</returns>
        public bool UpdateCancellation(int id, Cancellation cancellation)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Entry(cancellation).State = EntityState.Modified;

                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CancellationExists(id))
                        {
                            return false;
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
            catch (CancellationException)
            {

                throw new CancellationException("Id Doesnt Exists");
            }
        }



        /// <summary>
        /// method to find whether the record exist in cancellation table or not
        /// </summary>
        /// <param name="id">Cancellation ID is the parameter</param>
        /// <returns>return bool value accordingly</returns>
        private bool CancellationExists(int id)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    return db.Cancellations.Count(e => e.CancellationId == id) > 0;
                }
            }
            catch (CancellationException)
            {

                throw new CancellationException("Cancellation Doesnt Exists");
            }
        }


        /// <summary>
        /// Method to add cancelled tickets
        /// </summary>
        /// <returns></returns>
        public bool AddCancellation(Cancellation cancellation)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {

                    if ((db.Cancellations.Count(c => c.CancellationId == cancellation.CancellationId)) == 0)
                    {
                        //Add command that will add to database
                        db.Cancellations.Add(cancellation);
                        db.SaveChanges();
                        db.Configuration.LazyLoadingEnabled = false;
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (CancellationException)
            {

                throw new CancellationException("Internal Error");
            }

        }


        ///// <summary>
        ///// Method delete the existing customer account
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public bool DeleteCancellationById(int id)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                Cancellation cancellation = db.Cancellations.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                if (cancellation == null)
                {
                    return false; ;
                }

                db.usp_DeleteCancelledTicketById(id);
                db.SaveChanges();

                return true;
            }
        }


      /// <summary>
      /// Method that automtically add records to cancellation table and update refunds
      /// </summary>
      /// <param name="bookingId">Takes bookigId as parameters</param>
      /// <returns>Bool value</returns>

        public bool CancellationAuto(int bookingId)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    Booking book = db.Bookings.Find(bookingId);
                    if (book == null)
                    {
                        throw new CancellationException("No Bookings Available");
                    }
                    //checks whether booking is cancelled or not
                    if (book.TicketStatus.Equals("CANC"))
                    {
                        throw new CancellationException("Ticket Already Cancelled");
                    }
                    else
                    {
                        Cancellation can = new Cancellation()
                        {
                            FlightId = book.FlightId,
                            BookingId = bookingId,
                            DateOfCancellation = DateTime.Now,
                            //checks whether date of journey is from which date
                            RefundAmount = CalculationOfRefund(bookingId, book.DateOfJourney <= DateTime.Now ? 0 : 1)
                        };
                        db.Cancellations.Add(can);
                        db.SaveChanges();
                        //After adding the object to cancellation table, the status of booking will be updated to Cancelled in bookings table
                        book.TicketStatus = "CANC";
                        book.TicketFare = book.TicketFare - can.RefundAmount;
                        db.SaveChanges();


                        Flight flight = db.Flights.Find(can.FlightId);
                        //To store null we are giving ?
                        int? flightAvailableSeats = flight.AvailableSeats;
                        int? noOfSeats = book.NoOfSeats;
                        // Converting int? to int as Available Seats has integer datatype
                        flight.AvailableSeats = (int)(flightAvailableSeats + noOfSeats);
                        //updating the available seats in flight table
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (CancellationException ex)
            {

                throw new CancellationException(ex.Message);
            }

        }



        /// <summary>
        ///  Method to calculate the refund amount
        /// </summary>
        /// <param name="bookingId">Booking Id is the parameter which will fetch the total fare of ticket </param>
        /// <param name="flightStatus"></param>
        /// <returns>return refund amount</returns>

        private decimal CalculationOfRefund(int bookingId, int flightStatus)
        {
            try
            {
                using (AirlineDBEntities db = new AirlineDBEntities())
                {
                    decimal refund = 0;
                    if (flightStatus == 0)
                    {
                        return refund;
                    }
                    else
                        refund = (db.Bookings.Find(bookingId).TicketFare * 75) / 100;
                    return refund;
                }
            }
            catch (CancellationException)
            {

                throw new CancellationException("Id Doesnt Exists");
            }
        }
    }
}