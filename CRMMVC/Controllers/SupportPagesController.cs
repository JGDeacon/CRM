using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMMVC.Controllers
{
    public class SupportPagesController : Controller
    {
        // GET: SupportPages
        public ActionResult ContactSales()
        {
            return View();
        }
        // GET: SupportPages
        public ActionResult Testimonials()
        {
            return View();
        }
        // GET: SupportPages
        public ActionResult UseCases()
        {
            return View();
        }
    }
}