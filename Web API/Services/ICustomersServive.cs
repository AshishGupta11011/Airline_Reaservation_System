
//-----------------------------------------------------------------------------------------
// Developer    :  ASHISH GUPTA
// File Name    :  ICustomersService.cs
// Create Date  :  <17th May,2020>
// Last Updated :  <19th May,2020>
// Description  :  To perform business logic and accordingly return response to CustomersController
// Task         :  CRUD  opreation with Customers table in database
// ------------------------------------------------------------------------------------------


using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// interface for CustomersService 
    /// </summary>
    interface ICustomersServive
    {
        /// <summary>
        /// Method to get details of all customers
        /// </summary>
        /// <returns> List<Customer></returns>
        List<Customer> GetALLCustomers();

        /// <summary>
        /// method to get detail of a customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer GetCustomerById(int id);
        /// <summary>
        /// Method to update Customers details 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        bool UpdateCustomer(int id, Customer customer);

        /// <summary>
        /// Method to Create customers account
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        bool AddCustomer(Customer customer);

        /// <summary>
        /// Method to delete Customers Account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteCustomerById(int id);
        /// <summary>
        /// Method to check Whether Customer Exists By using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CustomerExists(int id);

    }
}
