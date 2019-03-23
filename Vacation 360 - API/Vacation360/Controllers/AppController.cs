using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vacation360.Controllers
{
    public class AppController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }
    }
}