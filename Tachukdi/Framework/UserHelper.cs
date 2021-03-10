using Tachukdi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
