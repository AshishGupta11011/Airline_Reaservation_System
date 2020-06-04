//=============================================
//  Developer:	<Mamta Chauhan>
//  Create date: <17th May,2020>
//  last updated date:<20th may,2020>
//  Description : To perform business logic and accordingly return response to PassengerController
// Task : CRUD operation with Passenger
//=============================================using System;
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
using Airline_Reservation.web.Models;

namespace Airline_Reservation.web.Controllers
{
    /// <summary>
    /// passengers class derived from ApiController
    /// </summary>
    public class PassengersController : ApiController
    {
        //This is class of AirlineDbEntities
        private AirlineDBEntities db;


        /// <summary>
        /// created constructor PassengersController
        /// </summary>
        public PassengersController()
        {
            //Creation of DBContext class object
            db = new AirlineDBEntities();
        }



        /// <summary>
        /// Get a single passenger data by passengerId
        /// GET:api:/Passengers/5
        /// </summary>
        /// <param name="passengerid ">passenger details from passenger id</param>
        /// <returns>Received data in database</returns>

        //GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]

        [Route("api/PassengersNo")]
        public int NoOfPassengers(int id)

        {
            //get gthe details of passenger by existing booking id
            Booking booking = db.Bookings.Where<Booking>(b => b.BookingId.Equals(id)).FirstOrDefault();
            int noOfSeats = booking.NoOfSeats;

            return noOfSeats;
        }

        /// <summary>
        /// Get all the Passenger details avialable
        /// GET :api/Passengers
        /// </summary>
        /// <returns>Data from Passengers table</returns>
        // GET: api/Passengers
        public IQueryable<Passenger> GetPassengers()
        {
            //
            return db.Passengers;
        }

        /// <summary>
        /// Get a single passenger data by passengerId
        /// GET:api:/Passengers/5
        /// </summary>
        /// <param name="passengerid ">passenger details from passenger id</param>
        /// <returns>Received data in database</returns>

        // GET: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult GetPassenger(long id)
        {
            //get gthe details of passenger by existing booking id
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return NotFound();
            }

            return Ok(passenger);
        }

        /// <summary>
        /// Updated the existing passenger details
        /// </summary>
        /// <param name="id">Updated by comparing the passenger details by passenger id</param>
        /// <param name="passenger">Show the edited/updated deatils of passenger</param>
        /// <returns>updated/edited passenger deatils stored in database </returns>
        // PUT: api/Passengers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPassenger(long id, Passenger passenger)
        {
            //check for validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //if the updated passenger id do not match with the given id it show BadRequest
            if (id != passenger.PassengerId)
            {
                return BadRequest();
            }

            //show the updated/edited details of passengers
            db.Entry(passenger).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            //Exception thrown by when it was expected that SaveChanges for
            //an passenger would result in a database update but in fact no rows in
            //the database were affected
            catch (DbUpdateConcurrencyException)
            {
                //if passenger id do not match(not existed) it return NOTFound
                if (!PassengerExists(id))
                {
                    return NotFound();
                }
                //otherwise throw the exception(DbUpdateConcurrencyException)
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adding the passenger details in database 
        /// </summary>
        /// <param name="passenger">all the properties of passenger while adding in passenger record</param>
        /// <returns>return the record of single passenger
        /// </returns>
        // POST: api/Passengers
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult PostPassenger(Passenger passenger)
        {

            //check for validation of passenger
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //insert data into database
            db.Passengers.Add(passenger);
            //data will stored in passenger
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = passenger.PassengerId }, passenger);
        }

        /// <summary>
        /// Deleting the record of passenger by  pssenger id
        /// </summary>
        /// <param name="id"> passenger id matched with the id to be delete  </param>
        /// <returns>record will be deleted from database</returns>

        // DELETE: api/Passengers/5
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult DeletePassenger(long id)
        {
            //finding the id from passenger database 
            Passenger passenger = db.Passengers.Find(id);
            //if passenger id is null(not found ) it return "NOTFound"
            if (passenger == null)
            {
                return NotFound();
            }
            //else if id match the id to be delete will remove the details of that passenger id
            db.Passengers.Remove(passenger);
            db.SaveChanges();
            //successfully deleted details of passenger
            return Ok(passenger);
        }
        /// <summary>
        /// throwing out unmanaged or managed depends upon the bool type
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            //if boolean is true dispose will release both managed and unmanaged rsource
            if (disposing)
            {
                db.Dispose();
            }
            //else dispose will release only unmanaged resource
            base.Dispose(disposing);

        }
        public bool PassengerExists(long id)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                return db.Passengers.Count(e => e.PassengerId == id) > 0;
            }
        }
    }
}