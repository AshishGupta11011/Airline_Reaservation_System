
//-----------------------------------------------------------------------------------------
// Developer: ASHISH GUPTA
// File Name : User.cs
// Create Date: <16th May,2020>
// Last Updated Date: <20th May,2020>
// Description:Class To Represent Users's Credential Object
// ------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airline_Reservation.web.Models
{
    /// <summary>
    /// Class for User's Credentials
    /// </summary>
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}