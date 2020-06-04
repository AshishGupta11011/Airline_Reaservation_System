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
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Airline_Reservation.web.CustomExceptions;
using Airline_Reservation.web.Models;
using Airline_Reservation.web.Services;

namespace Airline_Reservation.web.Controllers
{
    public class CancellationsController : ApiController
    {

        //declare AuthenticationService type instance variable
        //AuthenticationService Authservice;

        //declare CustomersService type instance variable
        CancellationService cancService;
        CancellationsController()
        {

            //declare CustomersService type instance variable
            cancService = new CancellationService();
        }


        /// <summary>
        /// A method of controller that returns list of cancelled tickets
        /// </summary>
        /// <returns>List of Cancelled tickets</returns>
        // GET: api/Cancellations
        public List<Cancellation> GetCancellations()
        {
            try
            {
                List<Cancellation> cancellations = cancService.GetALLCancellation();
                return cancellations;
            }
            catch (CancellationException)
            {

                throw;
            }
        }


        /// <summary>
        /// A method of controller that returns only one entry based on incoming  cancellation id
        /// </summary>
        /// <param name="id">cancellation id the parameter</param>
        /// <returns>Returns a single entry from cancellation table</returns>
        // GET: api/Cancellations/5
        [ResponseType(typeof(Cancellation))]
        public IHttpActionResult GetCancellationById(int id)
        {
            try
            {
                if (!cancService.DeleteCancellationById(id))
                {
                    return NotFound();
                }

                return Ok("Cancellation deleted");
            }
            catch (CancellationException)
            {

                throw;
            }
        }


        /// <summary>
        /// A method of controller that updates the value of particular entry obtained by it's cancellation id
        /// </summary>aar
        /// <param name="id">Cacellation Id is the parameter</param>
        /// <param name="cancellation"></param>
        /// <returns>Returns a status code confirming the update</returns>
        // PUT: api/Cancellations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCancellation(int id, Cancellation cancellation)
        {
            //Checking whether the model passes to every validation applied
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Checks whether the incoming Id and the Id that is to be updated are equal or not
            if (id != cancellation.CancellationId)
            {
                return BadRequest();
            }

            //One amonng the five states of entity state that is being tracked by the entity for modification
            try
            {
                bool isUpdated = cancService.UpdateCancellation(id, cancellation);
                if (isUpdated) { return StatusCode(HttpStatusCode.NoContent); }
                else { return NotFound(); }
            }
            catch (CancellationException)
            {

                throw;
            }
        }


        /// <summary>
        /// A method of controller that inserts value into the cancellations table
        /// </summary>
        /// <param name="cancellation">Recieves an object from angular program that is to be inserted into the cancellation table</param>
        /// <returns>Returns an URI havinf default Api along with the Cancellation Id</returns>
        // POST: api/Cancellations
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCancellation(Cancellation cancellation)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (cancService.AddCancellation(cancellation))
                {
                    return CreatedAtRoute("DefaultApi", new { id = cancellation.CancellationId }, cancellation);
                }
                else
                {
                    return Conflict();
                }
            }
            catch (CancellationException)
            {

                throw;
            }
        }



        /// <summary>
        /// A method of controller that deletes a particular records from cancellation tables according to the incoming Cancellation id
        /// </summary>
        /// <param name="id">Receives an cancellation ID for checking in cancellation table</param>
        /// <returns>Returns an object to angular</returns>
        // DELETE: api/Cancellations/5
        [ResponseType(typeof(Customer))]
        [Authorize]
        public IHttpActionResult DeleteCancellation(int id)
        {

            try
            {

                if (!cancService.DeleteCancellationById(id))
                {
                    return NotFound();
                }

                return Ok("Cancellation deleted");
            }
            catch (CancellationException)
            {

                throw;
            }

        }



        /// <summary>
        ///A method of controller that shows the details of cancelled tickets
        /// </summary>
        /// <param name="bookingId">Booking Id of the selected records is the parameter</param>
        /// <returns>Returns Ok status</returns>
        [HttpGet]
        [Route("api/Cancellations/CancellationDetails/{BookingId}")]
        public IHttpActionResult CancellationDetails(int bookingId)
        {
            try
            {

                bool cancellationAdded = cancService.CancellationAuto(bookingId);
                if (cancellationAdded)
                {
                    return Ok("Added Sucessfully");
                }
                else
                    return BadRequest();

            }
            catch (CancellationException)
            {

                throw ;
            }
        }
    }
}