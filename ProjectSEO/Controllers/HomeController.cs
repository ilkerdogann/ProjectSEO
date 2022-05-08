using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSEO.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Response.Status = "301 Kalıcı Yönlendirme";
            //Response.StatusCode = 301;
            //Response.StatusDescription = "Kalıcı Yönlendirme";
            //Response.AddHeader("Location", "www.?.com");
            //Response.End();

            return View();
        }
    }
}