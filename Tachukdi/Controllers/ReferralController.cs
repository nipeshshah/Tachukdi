using Tachukdi.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace Tachukdi.Controllers
{
    public class ReferralController : BaseController
    {
        [HttpGet]
        [Route("api/refer/myid")]
        public IHttpActionResult GetMyReferralId()
        {
            if (ExtractToken(User))
            {
                string referralcode = _helpers.ReferralHelper.GetMyReferralId(_auth.userid);
                return Ok(new { ReferralCode = referralcode, OtherReferralCode = _auth.userid });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("api/refer/all")]
        public IHttpActionResult GetMyReferrals()
        {
            if (ExtractToken(User))
            {
                List<ReferralUserModal> referralUsers = _helpers.ReferralHelper.GetMyReferrals(_auth.userid);
                return Ok(referralUsers);
            }
            else
                return Unauthorized();
        }
    }


}
