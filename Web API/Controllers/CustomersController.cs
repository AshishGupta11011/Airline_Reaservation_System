
//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  CustomersController.cs
// Create Date  :  <16th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  To Handles Client requests and  perform Business logic and  accordingly return response to Client.
// Task         :  CRUD  opreation with Customers table in database
// ------------------------------------------------------------------------------------------

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
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;


namespace Airline_Reservation.web.Controllers
{

    public class CustomersController : ApiController
    {
        //declare AuthenticationService type instance variable
        //AuthenticationService Authservice;

        //declare CustomersService type instance variable
        CustomersService custService;
        CustomersController()
        {
            
            //declare CustomersService type instance variable
            custService = new CustomersService();
        }

        // GET: api/Customers
        /// <summary>
        /// method to get list of all customers
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ResponseType(typeof(List<Customer>))]
        public IHttpActionResult GetCustomers()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var role = claims.Where(r => r.Type == "Email").FirstOrDefault()?.Value;
                if (role == "ashishguptaid@gmail.com")
                {
                   
                    return Ok(custService.GetALLCustomers());
                }
                else { return Unauthorized(); }
            }
            else { return Unauthorized(); }
        }

        // GET: api/Customers/5
        /// <summary>
        /// method to get customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      //  [Authorize]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = custService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }


        // PUT: api/Customers/5
        /// <summary>
        /// method to update cutomer details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
     //   [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            try
            {
                bool isUpdated = custService.UpdateCustomer(id, customer);
                if (isUpdated) { return StatusCode(HttpStatusCode.NoContent); }
                else { return NotFound(); }
            }
            catch (ApplicationException)
            {

                throw;
            }

        }
       

        // POST: api/Customers
        /// <summary>
        /// Method to Add Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (custService.AddCustomer(customer))
            {
                return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
            }
            else
            {
                return Conflict();
            }
        }

        // DELETE: api/Customers/5
        /// <summary>
        /// Method to delete Customer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Customer))]
       [Authorize]
        public IHttpActionResult DeleteCustomer(int id)
        {


            if (!custService.DeleteCustomerById(id))
            {
                return NotFound();
            }

            return Ok("Customer account deleted");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                custService.Dispose();
               // db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}