using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VNRWebMVC.Controllers
{
    [Authorize(Roles = "Admin,Teacher,Student")]
    [RoutePrefix("TimeTable")]
    public class TimeTableController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}