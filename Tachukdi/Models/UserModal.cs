using System;

namespace Tachukdi.Models
{
  public class UserModal
  {
    public int id { get; set; }
    public string mobileno { get; set; }
    public string ReferredByMobile { get; set; }
    public string Email { get; set; }
    public string ReferralCode { get; set; }
    public string DisplayName { get; set; }
    public string City { get; set; }
  }

  public class PointTransactionModal
  {
    public DateTime? TransactionDate { get; set; }
    public string Description { get; set; }
    public int? Points { get; set; }
  }

  public class CategoryModal
  {
    public int CateId { get; set; }
    public string CateName { get; set; }
  }

  public class CityModal
  {
    public int CityId { get; set; }
    public string CityName { get; set; }
    public string State { get; set; }
  }

  public class ReferralUserModal
  {
    public string DisplayName { get; set; }
    public object JoinedOn { get; set; }
    public string Mobile { get; set; }
  }

  public class BlockCityModal
  {
    public int CityId { get; set; }
    public string City { get; set; }
    public int BlockCount { get; set; }
  }

  public class BlockCategoryModal
  {
    public int CityId { get; set; }
    public int CatId { get; set; }
    public int BlockCount { get; set; }
    public string Category { get; set; }
  }

  public class BlockModal
  {
    public string Content { get; set; }
    public string MobileNo { get; set; }
    public DateTime CreatedDate { get; set; }
  }

  public class RegisterRequestModal
  {
    public string referredbyCode { get; set; }
    public string mobileno { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string displayname { get; set; }
    public string city { get; set; }
  }
  public class BlockRequestModal
  {
    public int CatId { get; set; }
    public int CityId { get; set; }
    public string Content { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
  }
}
