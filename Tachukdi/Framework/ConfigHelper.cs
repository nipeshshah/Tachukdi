using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tachukdi.Models;

namespace Tachukdi.Framework
{
    public class ConfigHelper : BaseHelper
    {
        internal List<CityModal> GetAllCities()
        {
            return _db.GetAllCities();
        }

    internal CityModal GetCityByCityName(string cityname)
    {
      return _db.GetCityByCityName(cityname);
    }

    internal List<CategoryModal> GetAllCategories()
        {
            return _db.GetAllCategories();
        }
    }

    public class ReferralHelper : BaseHelper
    {
        internal string GetMyReferralId(string mobileno)
        {
            return _db.GetUserByMobileNo(mobileno).ReferralCode;
        }

        internal List<ReferralUserModal> GetMyReferrals(string mobileno)
        {
            List<ReferralUserModal> referralUsers = _db.GetAllReferrals(mobileno);
            return referralUsers;
        }
    }

    public class PointHelper:BaseHelper
    {
        internal List<PointTransactionModal> GetTopNTransaction(string mobileno, int topCount)
        {
            List<PointTransactionModal> pointTransactions = _db.GetTopNPointTransaction(mobileno, topCount);
            return pointTransactions;
        }

        internal int GetTotalPontBalance(string mobileno)
        {
            int totalPoints = _db.GetTotalPontBalance(mobileno);
            return totalPoints;
        }

        public void AddPoint(string mobileno, string description, int points)
        {
            _db.AddPoint(mobileno, description, points);
        }

        public void DeductPoint(string mobileno, string description, int points)
        {
            _db.AddPoint(mobileno, description, -1 * points);
        }
    }
}
