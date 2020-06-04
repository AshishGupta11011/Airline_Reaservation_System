

//-----------------------------------------------------------------------------------------
// Developer     : ASHISH GUPTA
// File Name     : AuthenticateController.cs
// Create Date   : <17th May,2020>
// Last Updated  : <20th May,2020>
// Description   : To Handle Authentication Based requests and  perform Business logic and  accordingly return response to Client.
// Task          : To Verify The user Credentials And Assign Json Web Tokens 
// ------------------------------------------------------------------------------------------


using Airline_Reservation.web.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace Airline_Reservation.web.Controllers
{
    /// <summary>
    /// Controller for Authentication for providing registration and Login functionalities
    /// </summary>
    public class AuthenticateController : ApiController
    {
        //property to define the role of person
        

        private AirlineDBEntities db;

        public AuthenticateController()
        {
            db = new AirlineDBEntities();
        }


        // POST: api/Authenticate
        /// <summary>
        /// Method  to return JWT Token to registered User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //[ResponseType(typeof(User))]
        public object PostAuthenticate(User user)
        {
            string role = "user";
            Customer isUser =  db.Customers.Where(c => c.Email == user.Email).FirstOrDefault();

            if (isUser != null)
            {
                if (isUser.Pwd == user.Password)
                {
                    if (user.Email == "ashishguptaid@gmail.com") { role = "admin"; }

                    string token = GetToken(user);
                    return new { res = token ,_role = role, id = (int)isUser.CustomerId };
                    // return  new { data = GetToken(user) , Id =  isUser.CustomerId };
                    
                        //CreatedAtRoute("DefaultApi",token, isUser);

                }
                else
                {
                    return Content(HttpStatusCode.Unauthorized, "Invalid Password");
                }
               
            }
            else
            {
                return Content(HttpStatusCode.NotFound, "User Does Not Exists");
            }
            
        }

        /// <summary>
        /// Method to generate  Json Web Tokens(JWT)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
         public string GetToken(User user)
        {

            string key = "ashish1234jhagkgdjgdfkjagfddhldfkughlkjdsahgkjg";
            var issuer = "www.mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permclaims = new List<Claim>();
            permclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permclaims.Add(new Claim("Email", $"{user.Email}"));
            permclaims.Add(new Claim("valid", "1"));

            var token = new JwtSecurityToken(issuer,
                issuer,
                permclaims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: credentials);

            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;


        }
        
    }
}
