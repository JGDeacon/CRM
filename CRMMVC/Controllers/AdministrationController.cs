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
            var model = service.GetAllDepartments();
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
        [HttpGet]
        public ActionResult DepartmentEdit(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Department ID.";
                return RedirectToAction("Departments");
            }
            var service = CreateAdministrationService();
            int validID = (int)id;
            var model = service.GetDepartment(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Department ID.";
                return RedirectToAction("Departments");
            }
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentEdit(EditDepartment model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateAdministrationService();
            if (service.EditDepartment(model.DepartmentID, model))
            {
                return RedirectToAction("Departments");
            }
            return View(model);
        }

        //DELETE: Departments
        [HttpGet]
        public ActionResult DepartmentDelete(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Department ID.";
                return RedirectToAction("Departments");
            }
            var service = CreateAdministrationService();
            int validID = (int)id;
            var model = service.GetDepartment(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Department ID.";
                return RedirectToAction("Departments");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentDelete(int id)
        {
            var service = CreateAdministrationService();
            if (!service.DeleteDepartment(id))
            {
                TempData["InvalidAction"] = "Delete Action Failed.";
                return RedirectToAction("Departments");
            }
            TempData["SaveResult"] = $"Department {id} was deleted.";
            return RedirectToAction("Index");
        }


        //GET: Users
        public ActionResult Users()
        {
            var service = CreateAdministrationService();
            var model = service.GetAllUsers();
            return View(model);
        }
        //
    }
}