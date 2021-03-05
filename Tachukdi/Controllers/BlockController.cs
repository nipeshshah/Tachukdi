using Tachukdi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Tachukdi.Controllers
{
  [EnableCors(origins: "*", headers: "*", methods: "*")]
  public class BlockController : BaseController
    {
        [Route("api/blocks/city")]
        public IHttpActionResult GetBlockCities()
        {
            List<BlockCityModal> blockCities = _helpers.BlockHelper.GetBlockCityListWithCount();
            return Ok(blockCities);
        }

        [Route("api/blocks/city/{cityid}")]
        public IHttpActionResult GetBlockInCity(int cityid)
        {
            List<BlockCategoryModal> blockCities = _helpers.BlockHelper.GetBlockCategoryListWithCount(cityid);
            return Ok(blockCities);
        }

        [Route("api/blocks/city/{cityid}/cat/{catid}")]
        public IHttpActionResult GetBlockInCityInCat(int cityid, int catid)
        {
            List<BlockModal> blockCities = _helpers.BlockHelper.GetBlockList(cityid, catid);
            return Ok(blockCities);
        }

        [Route("api/blocks/submit")]
        public IHttpActionResult AddNewBlock(BlockRequestModal block)
        {
            if (ExtractToken(User))
            {
                _helpers.BlockHelper.AddNewBlock(_auth.userid, block);
                return Ok(true);
            }
            else
                return Unauthorized();
        }
    }
}
