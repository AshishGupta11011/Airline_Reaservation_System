using Airline_Reservation.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Airline_Reservation.web.Services
{
    /// <summary>
    /// Authentication services that contains methods related to user accounts
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        /// <summary>
        /// Method to check if user is Admin or Customer
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public bool IsAdmin(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                using (AirlineDBEntities db = new AirlineDBEntities()) {

                    IEnumerable<Claim> claims = identity.Claims;
                    var role = claims.Where(r => r.Type == "Email").FirstOrDefault()?.Value;
                    if (role == "ashishguptaid@gmail.com")
                    {
                        return true;
                    }
                    else { return false; }
                }
            }
            else { return false; }
        }
    }
}