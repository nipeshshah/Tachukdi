using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachukdi.Models;

namespace Tachukdi.Framework
{
    public class BlockHelper: BaseHelper
    {
        internal List<BlockCityModal> GetBlockCityListWithCount()
        {
            List<BlockCityModal> blockCities = _db.GetBlockCityListWithCount();
            return blockCities;
        }

        internal List<BlockCategoryModal> GetBlockCategoryListWithCount(int cityid)
        {
            List<BlockCategoryModal> blockCategories = _db.GetBlockCategoryListWithCount(cityid);
            return blockCategories;
        }

        internal List<BlockModal> GetBlockList(int cityid, int catid)
        {
            List<BlockModal> blocks = _db.GetBlockList(cityid, catid);
            return blocks;
        }

        internal bool AddNewBlock(string mobileno, BlockRequestModal block)
        {
            _db.AddNewBlock(mobileno, block);
            return true;
        }
    }
}
