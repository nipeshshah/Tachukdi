using System.Web.Http;
using Tachukdi.Models;

namespace Tachukdi.Controllers
{
  public class UserController : BaseController
  {
    //[Route("api/user/getname")]
    //public String GetName1()
    //{
    //    ExtractToken(User);
    //    if (User.Identity.IsAuthenticated)
    //    {
    //        var identity = User.Identity as ClaimsIdentity;
    //        if (identity != null)
    //        {
    //            IEnumerable<Claim> claims = identity.Claims;
    //        }
    //        return "Valid";
    //    }
    //    else
    //    {
    //        return "Invalid";
    //    }
    //}

    [Route("api/user/join")]
    public IHttpActionResult Register(RegisterRequestModal registerUser)
    {
      //Check if User Exists
      //if User Not exists then create InActiveUserWithCode
      //send OTP To user
      if(!_helpers.UserHelper.IsUserExists(registerUser.mobileno))
      {
        _helpers.UserHelper.CreateUser(registerUser.email, registerUser.mobileno, registerUser.password,
            registerUser.displayname, registerUser.referredbyCode, out string otp);
        _helpers.UserHelper.SendOTP(registerUser.mobileno, otp);
      }
      return Ok();
    }

    [Route("api/user/otpconfirm")]
    public IHttpActionResult VerifyOTP(string mobileno, string otp, string password)
    {
      //if User exists and is InActiveUserWithCode
      //Validate OTP
      //Update user to Active
      if(_helpers.UserHelper.IsInActiveUserExists(mobileno))
      {
        if(_helpers.UserHelper.VerifyOTP(mobileno, otp))
        {
          _helpers.UserHelper.ActivateUser(mobileno, password);
          UserModal user = _helpers.UserHelper.GetUserProfile(mobileno);
          _helpers.PointHelper.AddPoint(mobileno, "Registration", 100);
          if(user.ReferredByMobile.Length > 0)
          {
            _helpers.PointHelper.AddPoint(user.ReferredByMobile, "Referral Points for " + mobileno, 5);
          }
        }
      }
      return Ok();
    }

    [Route("api/user/login")]
    public IHttpActionResult Login(string mobileno, string password)
    {
      //if user exists and is active
      //verify username and password
      //if found return success
      if(_helpers.UserHelper.IsUserExists(mobileno))
      {
        UserModal user = _helpers.UserHelper.LoginUserByIdPass(mobileno, password);
        if(user != null)
        {
          AuthController controller = new AuthController();
          return Ok(new {
            userid = user.id,
            mobileno = user.mobileno,
            token = controller.GetToken(user)
          });
        }
        else
        {
          return Unauthorized();
        }
      }
      else
      {
        return BadRequest("Invalid User Id or Password");
      }
    }

    //[Authorize]
    [Route("api/user/profile")]
    public IHttpActionResult UserProfile()
    {
      if(ExtractToken(User))
      {
        //if user exists and is active
        //verify username and password
        //if found return success
        if(_helpers.UserHelper.IsUserExists(_auth.userid))
        {
          UserModal userentity = _helpers.UserHelper.GetUserProfile(_auth.userid);
          return Ok(userentity);
        }
      }
      return Unauthorized();
    }

    //[Authorize]
    [Route("api/user/updateprofile")]
    public IHttpActionResult UpdateUserProfile()
    {
      //if user exists and is active
      //verify username and password
      //if found return success
      if(ExtractToken(User))
      {
        if(_helpers.UserHelper.IsUserExists(_auth.userid))
        {
          UserModal userModal = _helpers.UserHelper.GetUserProfile(_auth.userid);
          _helpers.UserHelper.UpdateUserProfile(userModal);
          return Ok(true);
        }
        else
        {
          return BadRequest("User Not Found");
        }
      }
      return Unauthorized();
    }
  }
}
