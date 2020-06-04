
//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  CustomersService.cs
// Create Date  :  <17th May,2020>
// Last Updated :  <20th May,2020>
// Description  :  To perform business logic and accordingly return response to CustomersController
// Task         :  CRUD  opreation with Customers table in database
// ------------------------------------------------------------------------------------------




using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// class  implements ICustomersService interface And manages CRUD operations on the Customers Table
    /// </summary>
    public class CustomersService : ICustomersServive
    {
        /// <summary>
        /// Method to get list of all customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetALLCustomers()
        {
            AirlineDBEntities db = new AirlineDBEntities();
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Customers.ToList();
            }
        }

        ///// <summary>
        ///// Method fetches the Customer corresponding to CustomerId
        ///// </summary>
        ///// <param name="id">id of customer</param>
        ///// <returns></returns>
        public Customer GetCustomerById(int id)
        {
            AirlineDBEntities db = new AirlineDBEntities();
            {
                db.Configuration.LazyLoadingEnabled = false;
                Customer customer = db.Customers.Find(id);
                return customer;
            }
        }

        ///// <summary>
        ///// Method updates the details of existing cutomer's account details 
        ///// </summary>
        ///// <param name="id">id of customer</param>
        ///// <param name="customer">customer object with customer details</param>
        ///// <returns></returns>
        public bool UpdateCustomer(int id, Customer customer)
        {
            AirlineDBEntities db = new AirlineDBEntities();
            {
                db.Configuration.LazyLoadingEnabled = false;
                Customer existingCustomer = db.Customers.Where(temp => temp.CustomerId == id).FirstOrDefault();
                
                

                try
                {
                    //update customer details if customer exists
                    if (existingCustomer != null)
                    {
                        existingCustomer.DOB = customer.DOB;
                        existingCustomer.Email = customer.Email;
                        existingCustomer.Name = customer.Name;
                        existingCustomer.PhoneNo = customer.PhoneNo;
                        existingCustomer.Pwd = customer.Pwd;
                        existingCustomer.ResidingAddress = customer.ResidingAddress;
                        existingCustomer.WalletBalance = customer.WalletBalance;

                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(id))
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

        /// <summary>
        /// Method create the new customer account
        /// </summary>
        /// <returns></returns>
        public bool AddCustomer(Customer customer)
        {
            AirlineDBEntities db = new AirlineDBEntities();
            {

                if ((db.Customers.Count(c => c.Email == customer.Email)) == 0)
                {

                    db.Customers.Add(customer);
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

        ///// <summary>
        ///// Method delete the existing customer account
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public bool DeleteCustomerById(int id)
        {
            AirlineDBEntities db = new AirlineDBEntities();
            {
                Customer customer = db.Customers.Find(id);
                db.Configuration.LazyLoadingEnabled = false;
                if (customer == null)
                {
                    return false; ;
                }

                db.Customers.Remove(customer);
                db.SaveChanges();

                return true;
            }
        }

        public void Dispose ()
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                db.Dispose();
            }
        }

        /// <summary>
        /// Method to check if custor account already exists or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CustomerExists(int id)
        {
            using (AirlineDBEntities db = new AirlineDBEntities())
            {
                return db.Customers.Count(e => e.CustomerId == id) > 0;
            }
        }
    }
}