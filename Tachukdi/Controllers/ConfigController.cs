using Tachukdi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Tachukdi.Controllers
{
    //[Authorize]
    public class ConfigController : BaseController
    {
        [HttpGet]
        [Route("api/config/cats")]
        public IHttpActionResult Categories()
        {
            if (ExtractToken(User))
            {
                List<CategoryModal> categories = _helpers.ConfigHelper.GetAllCategories();
                return Ok(categories);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        [Route("api/config/cites")]
        public IHttpActionResult Cities()
        {
            if (ExtractToken(User))
            {
                List<CityModal> cities = _helpers.ConfigHelper.GetAllCities();
                return Ok(cities);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
