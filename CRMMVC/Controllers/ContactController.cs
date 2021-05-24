using CRMModels.Create;
using CRMModels.Edit;
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
        [HttpGet]
        public ActionResult ContactEdit(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Department ID.";
                return RedirectToAction("Contact");
            }
            var service = CreateContactService();
            int validID = (int)id;
            var model = service.GetContact(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Contact ID.";
                return RedirectToAction("Contact");
            }
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult ContactEdit(EditContact model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateContactService();
            if (service.ContactEdit(model))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //DELETE: Contact
        [HttpGet]
        public ActionResult ContactDelete(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Contact ID.";
                return RedirectToAction("Cotancts");
            }
            var service = CreateContactService();
            int validID = (int)id;
            var model = service.GetContact(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Contact ID.";
                return RedirectToAction("Contacts");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactDelete(int id)
        {
            var service = CreateContactService();
            if (!service.ContactDelete(id))
            {
                TempData["InvalidAction"] = "Delete Action Failed.";
                return RedirectToAction("Contacts");
            }
            TempData["SaveResult"] = $"Department {id} was deleted.";
            return RedirectToAction("Index");
        }
    }
}