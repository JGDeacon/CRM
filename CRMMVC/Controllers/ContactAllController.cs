using CRMServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMMVC.Controllers
{
    public class ContactAllController : Controller
    {
        private ContactAllService CreateContactService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ContactAllService(userID);
            return service;
        }
        // GET: ContactAll
        public ActionResult Index()
        {
            var service = CreateContactService();
            var model = service.GetAllUserContacts();
            return View(model);
        }
    }
}