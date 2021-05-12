using CRMData;
using CRMModels.Create;
using CRMModels.Read;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices
{
    public class AdministrationService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userID;
        private readonly int _companyID;
        static HistoryService historyService = new HistoryService();

        public AdministrationService(Guid userID)
        {
            _userID = userID;
            _companyID = ctx.Users.Single(e => e.Id.ToString() == userID.ToString()).CompanyID;
        }
        //Departments Section
        public IEnumerable<ReadDepartments> GetDepartments()
        {
            var query = ctx.Departments.Where(e => e.CompanyID == _companyID).Select(g => new ReadDepartments
            {
                DepartmentID = g.DepartmentID,
                CompanyID = g.CompanyID,
                DepartmentName = g.DepartmentName,
                CreatedDateUTC = g.CreatedDateUTC,
                ModifiedDateUTC = g.ModifiedDateUTC
            });
            return query.ToList();
        }
        public bool AddDepartment(CreateDepartment model)
        {
            ctx.Departments.Add(new Departments
            {
                CompanyID = _companyID,
                DepartmentName = model.DepartmentName,
                CreatedDateUTC = DateTimeOffset.UtcNow
            }) ;
            return ctx.SaveChanges() == 1;
        }
        public bool EditDepartment(int id, CreateDepartment model)
        {
            Departments departments = ctx.Departments.Find(id);
            if (departments == null)
            {
                return false;
            }
            departments.DepartmentName = model.DepartmentName;
            departments.ModifiedDateUTC = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
        public bool DeleteDepartment(int id)
        {
            Departments department = ctx.Departments.Find(id);
            if (department == null)
            {
                return false;
            }
            ctx.Departments.Remove(department);
            return ctx.SaveChanges() == 1;
        }
        //Users Section
        //Used by Helpdesk and Admin Roles
        //Add User
        public bool AddUser(CreateUser model)
        {
            PasswordHasher ph = new PasswordHasher();
            ctx.Users.Add(new ApplicationUser
            {
                CompanyID = model.CompanyID,
                DepartmentID = model.DepartmentID,
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = ph.HashPassword(model.Password),
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedDateUTC = DateTimeOffset.UtcNow
            });
            return ctx.SaveChanges() == 1;
        }
        //Update User
        public bool EditUser(Guid id, CreateUser model)
        {
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser user = ctx.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            user.CompanyID = model.CompanyID;
            user.DepartmentID = model.DepartmentID;
            user.UserName = model.Username;
            user.Email = model.Email;
            user.PasswordHash = ph.HashPassword(model.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();
                        
            return ctx.SaveChanges() == 1;
        }
        //Disable User
        public bool DisableUser(Guid id)
        {
            ApplicationUser user = ctx.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnabled = true;
            return ctx.SaveChanges() == 1;
        }
        //Enable User
        public bool EnableUser(Guid id)
        {
            ApplicationUser user = ctx.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            user.LockoutEnabled = false;
            return ctx.SaveChanges() == 1;
        }
        //Change Password - This should be accessable by every non disabled user. This may move to a new service.
        public bool ResetPassword(CreatePassword model)
        {
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser user = ctx.Users.Single(e => e.Id.ToString() == _userID.ToString());
            user.PasswordHash = ph.HashPassword(model.Password);
            return ctx.SaveChanges() == 1;
        }
        //List Users
        public IEnumerable<ReadUser> GetAllUsers()
        {
            var query = ctx.Users.Where(e => e.CompanyID == _companyID).Select(f => new ReadUser
            {
                UserID = f.Id,
                CompanyID = f.CompanyID,
                DepartmentID = f.DepartmentID,
                Username = f.UserName,
                Email = f.Email,
                IsLocked = f.LockoutEnabled,
                CreatedDateUTC = f.CreatedDateUTC
            });
            return query.ToList();
        }

        //Get User
        public ReadUser GetUser(Guid id)
        {
            ApplicationUser user = ctx.Users.Find(id);
            if (user == null) //Check for a Null User
            {
                return null;
            }
            if (user.CompanyID != _companyID) //Check that user is in the same company as the requesting user.
            {
                return null;
            }

            ReadUser readUser = new ReadUser
            {
                UserID = user.Id,
                CompanyID = user.CompanyID,
                DepartmentID = user.DepartmentID,
                Username = user.UserName,
                Email = user.Email,
                IsLocked = user.LockoutEnabled,
                CreatedDateUTC = user.CreatedDateUTC
            };
            return readUser;
        }
        //DepartmentAccess Section
        //Pairs user to department in a company and assigns access.
        //Add DepartmentAccess
        public bool AddDepartmentAccess(CreateDepartmentAccess model)
        {
            ctx.DepartmentAccess.Add(new DepartmentAccess { DepartmentID = model.DepartmentID, CompanyID = _companyID, UserID = model.UserID, PermissionID = model.PermissionID, CreatedDateUTC = DateTimeOffset.UtcNow });
            return ctx.SaveChanges() == 1;
        }
        //Edit DepartmentAccess
        public bool EditDepartmentAccess(string id, CreateDepartmentAccess model)
        {
            DepartmentAccess departmentAccess = ctx.DepartmentAccess.Find(id);
            if (departmentAccess == null)
            {
                return false;
            }
            if (departmentAccess.CompanyID != _companyID) //Check that user is in the same company as the requesting user.
            {
                return false;
            }
            departmentAccess.DepartmentID = model.DepartmentID;
            departmentAccess.UserID = model.UserID;
            departmentAccess.PermissionID = model.PermissionID;
            departmentAccess.ModifiedDateUTC = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
        //Delete DepartmentAccess
        public bool DeleteDepartmentAccess(string id)
        {
            DepartmentAccess departmentAccess = ctx.DepartmentAccess.Find(id);
            if (departmentAccess == null)
            {
                return false;
            }
            if (departmentAccess.CompanyID != _companyID) //Check that user is in the same company as the requesting user.
            {
                return false;
            }
            ctx.DepartmentAccess.Remove(departmentAccess);
            return ctx.SaveChanges() == 1;
        }
        //List DepartmentAccess
        public IEnumerable<ReadDepartmentAccess> GetAllDepartmentAccess()

        //Get DepartmentAccess
    }
}
