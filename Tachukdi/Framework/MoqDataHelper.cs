using System;
using System.Collections.Generic;
using System.Linq;
using Tachukdi.Models;
using Tachukdi.Models.DBM;

namespace Tachukdi.Framework
{
  public interface IDataHelper
  {
  }

  public class MoqDataHelper : IDataHelper
  {
    tachukdiEntities _db = new tachukdiEntities();

    internal List<BlockCityModal> GetBlockCityListWithCount     ()
    {
      DateTime today = DateTime.UtcNow.Date;
      var blockcities =
          _db.TblBlocks
          .Where(t => t.StartDate <= today && t.EndDate >= today)
          .GroupBy(t => t.CityId).Select(x => new BlockCityModal()
          {
            City = _db.TblCities.Where(t => t.Id == x.Key).FirstOrDefault().CityName,
            BlockCount = x.Count(),
            CityId = x.Key.Value
          }).OrderBy(t => t.City);
      return blockcities.ToList();
    }

    internal List<BlockModal> GetBlockList(int cityid, int catid)
    {
      DateTime today = DateTime.UtcNow.Date;
      List<BlockModal> blockcates =
          _db.TblBlocks
          .Where(t => t.StartDate <= today && t.EndDate >= today
          && t.CityId == cityid && t.CategoryId == catid)
          .Select(x => new BlockModal()
          {
            Content = x.Content,
            MobileNo = x.MobileNo,
            CreatedDate = x.CreatedDate.Value
          }).OrderBy(t => t.CreatedDate).ToList();
      return blockcates;
    }

    internal void AddNewBlock(string mobileno, BlockRequestModal block)
    {
      int PointId = AddPoint(mobileno, "Register new block on " + DateTime.UtcNow,
          -1 * Convert.ToInt32(
                  Math.Ceiling(
                      (block.EndDate - block.StartDate).TotalDays
                  ) + 1
              ));

      TblBlock tblBlock = new TblBlock()
      {
        CategoryId = block.CatId,
        CityId = block.CityId,
        CreatedDate = DateTime.UtcNow,
        Title = block.Title,
        Content = block.Content,
        StartDate = block.StartDate,
        EndDate = block.EndDate,
        MobileNo = mobileno,
        UserId = _db.TblUsers.Where(t => t.MobileNo == mobileno).FirstOrDefault().Id,
        IsActive = true,
        PointTransactionId = PointId
      };

      _db.TblBlocks.Add(tblBlock);
      _db.SaveChanges();
    }

    internal List<BlockCategoryModal> GetBlockCategoryListWithCount(int cityid)
    {
      DateTime today = DateTime.UtcNow.Date;
      List<BlockCategoryModal> blockcates =
          _db.TblBlocks
         .Where(t => t.StartDate <= today && t.EndDate >= today && t.CityId == cityid)
          .GroupBy(t => t.CategoryId).Select(x => new BlockCategoryModal()
          {
            CatId = x.Key.Value,
            CityId = cityid,
            Category = _db.TblCategories.Where(t => t.Id == x.Key).FirstOrDefault().Title,
            BlockCount = x.Count()
          }).OrderBy(t => t.Category).ToList();
      return blockcates;
    }

    internal void CreateNewUser(string email, string mobileNo, string password, string displayname, string otp,
        string referralcode, string referredbyCode, DateTime registrationDate)
    {
      string referredByMobile = string.Empty;
      if(referredbyCode.Length > 0)
      {
        TblUser referredByUser = _db.TblUsers.Where(t => t.ReferralCode == referredbyCode).FirstOrDefault();
        referredByMobile = referredByUser != null ? referredByUser.MobileNo : string.Empty;
      }
      TblUser user = new TblUser()
      {
        Email = email,
        MobileNo = mobileNo,
        Name = displayname,
        Password = password,
        OTP = otp,
        ReferralCode = referralcode,
        ReferredByMobile = referredByMobile,
        Status = "InActive",
        JoiningDate = registrationDate
      };

      _db.TblUsers.Add(user);

      _db.SaveChanges();
    }

    internal int GetTotalPontBalance(string mobileno)
    {
      UserModal user = GetUserByMobileNo(mobileno);
      int? totalpoints = _db.TblPoints.Where(t => t.UserId == _db.TblUsers.Where(x => x.MobileNo == mobileno).FirstOrDefault().Id).Sum(t => t.Points);

      return totalpoints.HasValue ? totalpoints.Value : 0;
    }

    internal int AddPoint(string mobileno, string description, int points)
    {
      UserModal user = GetUserByMobileNo(mobileno);
      TblPoint tblPoint = new TblPoint()
      {
        TransactionDate = DateTime.UtcNow,
        UserId = _db.TblUsers.Where(x => x.MobileNo == mobileno).FirstOrDefault().Id,
        Description = description,
        Points = points
      };

      _db.TblPoints.Add(tblPoint);
      _db.SaveChanges();
      return tblPoint.Id;
    }

    internal List<PointTransactionModal> GetTopNPointTransaction(string mobileno, int topCount)
    {
      UserModal user = GetUserByMobileNo(mobileno);
      List<TblPoint> points = _db.TblPoints.OrderByDescending(t => t.TransactionDate)
        .Where(t => t.UserId == _db.TblUsers.Where(x => x.MobileNo == mobileno).FirstOrDefault().Id)
        .Take(topCount)
        .ToList();

      List<PointTransactionModal> pointTransactions = points.Select(t => new PointTransactionModal()
      {
        TransactionDate = t.TransactionDate,
        Description = t.Description,
        Points = t.Points
      }).ToList();

      return pointTransactions;
    }

    internal List<ReferralUserModal> GetAllReferrals(string mobileno)
    {
      List<TblUser> tblUsers = _db.TblUsers.Where(t => t.ReferredByMobile == mobileno).ToList();
      List<ReferralUserModal> referralUsers = tblUsers.Select(t => new ReferralUserModal()
      {
        DisplayName = t.Name,
        JoinedOn = t.JoiningDate
      }).ToList();

      return referralUsers;
    }

    internal List<CategoryModal> GetAllCategories()
    {
      List<TblCategory> tblCities = _db.TblCategories.ToList();
      List<CategoryModal> cityModals = tblCities.Select(t => new CategoryModal()
      {
        CateId = t.Id,
        CateName = t.Title
      }).ToList();
      return cityModals;
    }

    internal bool VerifyOTP(string mobileno, string otp)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == mobileno && t.OTP == otp).FirstOrDefault();
      if(tblUser != null)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    internal bool IsUserExists(string mobileno, bool onlyactive)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == mobileno).FirstOrDefault();
      if(tblUser != null)
      {
        if(onlyactive && tblUser.Status == "Active")
        {
          return true;
        }

        if(!onlyactive && tblUser.Status == "InActive")
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return false;
      }
    }

    internal bool UpdateUserProfile(UserModal userModal)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == userModal.mobileno).FirstOrDefault();
      if(tblUser != null)
      {
        tblUser.Email = userModal.Email;
        _db.SaveChanges();
        return true;
      }
      else
      {
        return false;
      }
    }

    internal UserModal GetUserByMobileNo(string mobileno)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == mobileno).FirstOrDefault();
      if(tblUser != null)
      {
        UserModal userModal = new UserModal();
        userModal.mobileno = tblUser.MobileNo;
        userModal.ReferredByMobile = tblUser.ReferredByMobile;
        userModal.ReferralCode = tblUser.ReferralCode;
        return userModal;
      }
      else
      {
        return null;
      }
    }

    internal bool ActivateUser(string mobileno, string password)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == mobileno).FirstOrDefault();
      if(tblUser != null)
      {
        tblUser.Status = "Active";
        tblUser.OTP = "";
        tblUser.Password = password;
        _db.SaveChanges();
        return true;
      }
      else
      {
        return false;
      }
    }

    internal UserModal GetUserByIdPass(string mobileno, string password)
    {
      TblUser tblUser = _db.TblUsers.Where(t => t.MobileNo == mobileno && t.Password == password).FirstOrDefault();
      if(tblUser != null)
      {
        UserModal userModal = new UserModal();
        userModal.mobileno = tblUser.MobileNo;
        userModal.ReferredByMobile = tblUser.ReferredByMobile;
        userModal.DisplayName = tblUser.Name;
        userModal.Email = tblUser.Email;
        userModal.City = tblUser.City;
        return userModal;
      }
      else
      {
        return null;
      }
    }

    internal List<CityModal> GetAllCities()
    {
      List<TblCity> tblCities = _db.TblCities.ToList();
      List<CityModal> cityModals = tblCities.Select(t => new CityModal()
      {
        CityId = t.Id,
        CityName = t.CityName,
        State = t.StateName
      }).ToList();
      return cityModals;
    }

    internal CityModal GetCityByCityName(string cityname)
    {
      TblCity tblCity = _db.TblCities.Where(t => t.CityName == cityname).FirstOrDefault();
      CityModal cityModal = new CityModal()
      {
        CityId = tblCity.Id,
        CityName = tblCity.CityName,
        State = tblCity.StateName
      };
      return cityModal;
    }
    
  }
}
