using Tachukdi.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Tachukdi.Controllers
{
    public class BaseController : ApiController
    {
        protected helpers _helpers { get; set; }
        protected AuthenticatedTokenModel _auth { get; set; }
        public BaseController()
        {
            _helpers = new helpers();
        }

        protected bool ExtractToken(IPrincipal user)
        {
            _auth = new AuthenticatedTokenModel();
            _auth.userid = "8976843988";
            _auth.name = "Demo User";
            _auth.email = "demo@user.com";
            return true;

            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;

                    _auth = new AuthenticatedTokenModel();
                    _auth.userid = identity.Claims.FirstOrDefault(c => c.Type == "userid").Value;
                    _auth.name = identity.Claims.FirstOrDefault(c => c.Type == "name").Value;
                    _auth.email = identity.Claims.FirstOrDefault(c => c.Type == "useremail").Value;
                }
                return true;
            }
            else
            {
                throw new System.UnauthorizedAccessException();
            }
        }
    }

    public class AuthenticatedTokenModel
    {
        public string name { get; set; }
        public string userid { get; set; }
        public string email { get; set; }
    }
}
