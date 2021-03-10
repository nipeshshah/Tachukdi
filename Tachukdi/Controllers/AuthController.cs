using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tachukdi.Models;

namespace Tachukdi.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("api/auth")]
        public string GetToken(UserModal user)
        //public Object GetToken(string mobileno, string email, string password)
        {
            string email = user.Email;
            string mobileno = user.mobileno;
            string key = "my_secret_key_12345"; //Secret key which will be used later during validation    
            var issuer = "http://mysite.com";  //normally this will be your site URL    

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", user.id.ToString()));
            permClaims.Add(new Claim("useremail", email));
            permClaims.Add(new Claim("userid", mobileno));
            permClaims.Add(new Claim("name", user.DisplayName));
            
            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, 
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt_token;
        }
    }
}
