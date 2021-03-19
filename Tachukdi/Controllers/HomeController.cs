using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tachukdi.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      //var d = System.IO.File.ReadAllText(@"E:\Organized D\Development\TravelProject\Copier\mvcversion\2\mvcversion62.txt");
      //byte[] b = Convert.FromBase64String(d);
      //System.IO.File.WriteAllBytes(@"E:\Organized D\Development\TravelProject\Copier\mvcversion\2\mvcversion62C.txt", b);
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}
