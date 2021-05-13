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
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Roles = "HelpDesk")]
    public class AdministrationController : Controller
    {
        private AdministrationService CreateAdministrationService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new AdministrationService(userID);
            return service;
        }
        // GET: Adminstration
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        //GET: Departments
        public ActionResult Departments()
        {
            var service = CreateAdministrationService();
            var model = service.GetDepartments();
            return View(model);
        }
        public ActionResult DepartmentCreate()
        {
            return View();
        }
        //CREATE: Departments
        //Administration/Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentCreate(CreateDepartment model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateAdministrationService();
            if (service.AddDepartment(model))
            {
                TempData["SaveResult"] = $"{model.DepartmentName} was created.";
                return RedirectToAction("Departments");
            }
            ModelState.AddModelError("", $"{model.DepartmentName} could not be created.");
            return View(model);
        }
        //EDIT: Departments

        //DELETE: Departments
        //GET: Users
        public ActionResult Users()
        {
            var service = CreateAdministrationService();
            var model = service.GetAllUsers();
            return View(model);
        }
    }
}