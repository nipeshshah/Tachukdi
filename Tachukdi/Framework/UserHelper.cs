using System;
using Tachukdi.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Tachukdi.Framework
{
  public class BaseHelper
  {
    protected MoqDataHelper _db { get; set; }
    public BaseHelper()
    {
      _db = new MoqDataHelper();
    }
  }

  public class UserHelper : BaseHelper
  {
    public string CreateUser(string email, string mobileno, string password, string displayname, string referredbyCode, out string otp)
    {
      //string otp = "";
      Random random = new Random();
      otp = random.Next(100000, 999999).ToString();
      otp = "1234";
      string referralcode = "RFCA" + random.Next(11, 50) + "-" + random.Next(51, 99);
      _db.CreateNewUser(email, mobileno, password, displayname, otp, referralcode, referredbyCode, DateTime.UtcNow);
      return mobileno;
    }

    public UserModal LoginUserByIdPass(string mobileno, string password)
    {
      UserModal user = _db.GetUserByIdPass(mobileno, password);
      return user;
    }

    public bool SendOTP(string mobileno, string otp)
    {
      return true;
      //TODO
      string accountSid = "AC46507782a74540ce8fbb3ca78cc37350";
      string authToken = "f4cb58cae9793929e13721567e8d3318";

      TwilioClient.Init(accountSid, authToken);

      var message = MessageResource.Create(
          body: $"Hello to 1 Rupee Advertise. Your OPT is {otp}. Please verify to continue further.",
          from: new Twilio.Types.PhoneNumber("+17032913711"),
          to: new Twilio.Types.PhoneNumber("+918976843988")
      );
      //Send otp to mobileno using sms.
      return true;
    }

    public bool VerifyOTP(string mobileno, string otp)
    {
      bool IsVerified = _db.VerifyOTP(mobileno, otp);
      return IsVerified;
    }

    public bool IsUserExists(string mobileno)
    {
      bool IsActiveUserExists = _db.IsUserExists(mobileno, true);
      return IsActiveUserExists;
    }

    public bool IsInActiveUserExists(string mobileno)
    {
      bool IsUserExists = _db.IsUserExists(mobileno, false);
      return IsUserExists;
    }

    internal bool ActivateUser(string mobileno, string password)
    {
      bool IsUserActivated = _db.ActivateUser(mobileno, password);
      return IsUserActivated;
    }

    internal UserModal GetUserProfile(string mobileno)
    {
      UserModal user = _db.GetUserByMobileNo(mobileno);
      return user;
    }

    internal bool UpdateUserProfile(UserModal userModal)
    {
      return _db.UpdateUserProfile(userModal);
    }
  }
}
