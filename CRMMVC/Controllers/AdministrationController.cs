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
            //string userRole;
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
            return RedirectToAction("Departments");
        }
        //Department Access
        //GET: Department Access
        public ActionResult DepartmentAccess()
        {
            var service = CreateAdministrationService();
            var model = service.GetAllDepartmentAccess();
            return View(model);
        }

        //CREATE: Department Access
        public ActionResult DepartmentAccessCreate()
        {
            var service = CreateAdministrationService();
            IEnumerable<SelectListItem> departments =
                service.GetAllDepartments().Select(d =>
              new SelectListItem
              {
                  Value = d.DepartmentID.ToString(),
                  Text = d.DepartmentName
              });
            ViewBag.DepartmentID = departments;

            IEnumerable<SelectListItem> users =
                service.GetDepartmentAccessUsers().Select(d =>
               new SelectListItem
               {
                   Value = d.ID.ToString(),
                   
                   Text = d.Username
               });
            ViewBag.UserID = users;

            IEnumerable<SelectListItem> permissions =
                service.GetAllPermissions().Select(d =>
               new SelectListItem
               {
                   Value = d.PermissionID.ToString(),
                   Text = d.Access
               });
            ViewBag.PermissionID = permissions;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentAccessCreate(CreateDepartmentAccess model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateAdministrationService();
            if (!service.AddDepartmentAccess(model))
            {
                TempData["InvalidAction"] = "Add DepartmentAccess Action Failed.";
                return RedirectToAction("DepartmentAccess");
            }
            TempData["SaveResult"] = $"DepartmentAccess was created.";
            return RedirectToAction("DepartmentAccess");
        }
        //EDIT: Department Access
        [HttpGet]
        public ActionResult DepartmentAccessEdit(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Department Access ID.";
                return RedirectToAction("DepartmentsAccess");
            }
            var service = CreateAdministrationService();
            int validID = (int)id;
            var model = service.GetDepartmentAccess(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Department Access ID.";
                return RedirectToAction("DepartmentsAccess");
            }
            ViewBag.DepartmentID =
               service.GetAllDepartments().Select(d =>
             new SelectListItem
             {
                 Value = d.DepartmentID.ToString(),
                 Text = d.DepartmentName,
                 Selected = (model.DepartmentID == d.DepartmentID)? true : false
             }).ToList();
             

            IEnumerable<SelectListItem> users =
                service.GetDepartmentAccessUsers().Select(d =>
               new SelectListItem
               {
                   Value = d.ID.ToString(),

                   Text = d.Username
               });
            ViewBag.UserID = users;

            IEnumerable<SelectListItem> permissions =
                service.GetAllPermissions().Select(d =>
               new SelectListItem
               {
                   Value = d.PermissionID.ToString(),
                   Text = d.Access
               });
            ViewBag.PermissionID = permissions;
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentAccessEdit(EditDepartmentAccess model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateAdministrationService();
            if (service.EditDepartmentAccess(model))
            {
                return RedirectToAction("DepartmentAccess");
            }
            return View(model);
        }
        //DELETE: Department Access
        [HttpGet]
        public ActionResult DepartmentAccessDelete(int? id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid Department Access ID.";
                return RedirectToAction("Departments");
            }
            var service = CreateAdministrationService();
            int validID = (int)id;
            var model = service.GetDepartmentAccess(validID);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid Department Access ID.";
                return RedirectToAction("DepartmentAccess");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DepartmentAccessDelete(int id)
        {
            var service = CreateAdministrationService();
            if (!service.DeleteDepartmentAccess(id))
            {
                TempData["InvalidAction"] = "Delete Action Failed.";
                return RedirectToAction("DepartmentAccess");
            }
            TempData["SaveResult"] = $"Department Access {id} was deleted.";
            return RedirectToAction("DepartmentAccess");
        }

        //GET: Users
        public ActionResult Users()
        {
            var service = CreateAdministrationService();
            var model = service.GetAllUsers();
            return View(model);
        }
        //CREATE: User
        public ActionResult UserCreate()
        {
            var service = CreateAdministrationService();
            IEnumerable<SelectListItem> items =
                service.GetAllDepartments().Select(d =>
               new SelectListItem
               {
                   Value = d.DepartmentID.ToString(),
                   Text = d.DepartmentName
               });

            ViewBag.DepartmentID = items;
            IEnumerable<SelectListItem> roles =
                service.GetAllRoles().Select(d =>
               new SelectListItem
               {
                   Value = d.RoleID.ToString(),
                   Text = d.RoleName
               });

            ViewBag.RoleID = roles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(CreateUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var service = CreateAdministrationService();
            if (service.AddUser(model))
            {
                TempData["SaveResult"] = $"{model.Username} was created.";
                return RedirectToAction("Users");
            }
            ModelState.AddModelError("", $"{model.Username} could not be created.");
            return View(model);
        }
        //EDIT: User
        [HttpGet]
        public ActionResult UserEdit(Guid id)
        {
            if (id == null)
            {
                TempData["InvalidID"] = "Invalid User ID.";
                return RedirectToAction("Users");
            }
            var service = CreateAdministrationService();
            var model = service.GetUserEdit(id);
            if (model == null)
            {
                TempData["InvalidID"] = "Invalid User ID.";
                return RedirectToAction("Users");
            }
            IEnumerable<SelectListItem> editItems =
                service.GetAllDepartments().Select(d =>
               new SelectListItem
               {
                   Value = d.DepartmentID.ToString(),
                   Text = d.DepartmentName
               });

            ViewBag.DepartmentID = editItems;
            IEnumerable<SelectListItem> roles =
                service.GetAllRoles().Select(d =>
               new SelectListItem
               {
                   Value = d.RoleID.ToString(),
                   Text = d.RoleName
               });

            ViewBag.RoleID = roles;
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(EditUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreateAdministrationService();
            if (service.EditUser(model.UserID, model))
            {
                return RedirectToAction("Users");
            }
            return View(model);
        }

        //EDIT: Disable User

        //EDIT: Enable User
    }
}