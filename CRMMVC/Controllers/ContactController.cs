using CRMModels.Create;
using CRMServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMMVC.Controllers
{
    public class ContactController : Controller
    {
        private ContactService CreateContactService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new ContactService(userID);
            return service;
        }
        // GET: Contact
        public ActionResult Index()
        {
            var service = CreateContactService();
            var model = service.GetAllUserContacts();
            return View(model);
        }


        public ActionResult ContactCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactCreate(CreateContact model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateContactService();
            if (service.CreateContact(model))
            {
                TempData["SaveResult"] = $"{model.FirstName} {model.LastName} was created.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", $"{model.FirstName} {model.LastName} could not be created.");
            return View(model);
        }
    }
}