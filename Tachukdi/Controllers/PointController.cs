using Tachukdi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tachukdi.Controllers
{
    public class PointController : BaseController
    {
        [HttpGet]
        [Route("api/point/total")]
        public IHttpActionResult TotalPoints()
        {
            if (ExtractToken(User))
            {
                int pointBalance = _helpers.PointHelper.GetTotalPontBalance(_auth.userid);
                return Ok(pointBalance);
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("api/point/topten")]
        public IHttpActionResult TopTenPointTransactions()
        {
            if (ExtractToken(User))
            {
                List<PointTransactionModal> transactions = _helpers.PointHelper.GetTopNTransaction(_auth.userid, 10);
                return Ok(transactions);
            }
            else
                return BadRequest();
        }

        //public IHttpActionResult AddPoint()
        //{
        //    _helpers.PointHelper.AddPoint(_auth.userid, "Description", 1500);
        //    return Ok();
        //}

        //public IHttpActionResult DeductPoint()
        //{
        //    return Ok();
        //}
    }
}
