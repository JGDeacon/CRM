using CRMData;
using CRMModels.Create;
using CRMModels.Edit;
using CRMModels.Read;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        public IEnumerable<ReadDepartments> GetAllDepartments()
        {
            var query = ctx.Departments.Where(e => e.CompanyID == _companyID).Select(g => new ReadDepartments
            {
                DepartmentID = g.DepartmentID,
                CompanyID = g.CompanyID,
                DepartmentName = g.DepartmentName,
                CreatedDateUTC = g.CreatedDateUTC,
                ModifiedDateUTC = g.ModifiedDateUTC
            });
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                Method = $"GetAllDepartments()",
                stringID = null,
                Request = "GetAllDepartment()",
            };
            AddHistory(history);
            return query.ToList();
        }
        public EditDepartment GetDepartment(int id)
        {
            Departments department = ctx.Departments.Single(e => e.CompanyID == _companyID && e.DepartmentID == id);
            EditDepartment editDepartment = new EditDepartment
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
            };
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                Method = $"GetDepartment()",
                stringID = id.ToString(),
                Request = "GetDepartment()",
            };
            AddHistory(history);
            return editDepartment;
        }
        public bool AddDepartment(CreateDepartment model)
        {
            ctx.Departments.Add(new Departments
            {
                CompanyID = _companyID,
                DepartmentName = model.DepartmentName,
                CreatedDateUTC = DateTimeOffset.UtcNow
            }) ;
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                Method = $"AddDepartment(CreateDepartment model)",
                stringID = null,
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);            
        }
        public bool EditDepartment(int id, EditDepartment model)
        {
            Departments departments = ctx.Departments.Find(id);
            if (departments == null)
            {
                return false;
            }
            departments.DepartmentName = model.DepartmentName;
            departments.ModifiedDateUTC = DateTimeOffset.UtcNow;
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                Method = $"EditDepartment({id}, EditDepartment model)",
                stringID = id.ToString(),
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
        }
        public bool DeleteDepartment(int id)
        {
            Departments department = ctx.Departments.Find(id);
            if (department == null)
            {
                return false;
            }
            ctx.Departments.Remove(department);
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                stringID = id.ToString(),
                Request = $"DeleteDepartment({id})"
            };
            return AddHistory(history);
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
            //Sanataize Password in Model
            model.Password = "";
            model.ConfirmPassword = "";
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = null,
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
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
            //Sanataize Password in Model
            model.Password = "";
            model.ConfirmPassword = "";
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = id.ToString(),
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = id.ToString(),
                Request = $"DisableUser({id})"
            };
            return AddHistory(history);
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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = id.ToString(),
                Request = $"EnableUser({id})"
            };
            AddHistory(history);
            return ctx.SaveChanges() == 1;
        }
        //Change Password - This should be accessable by every non disabled user. This may move to a new service.
        public bool ResetPassword(CreatePassword model)
        {   
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser user = ctx.Users.Single(e => e.Id.ToString() == _userID.ToString());
            user.PasswordHash = ph.HashPassword(model.Password);
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = null,
                Request = "ResetPassword(CreatePassword model)"
            };
            AddHistory(history);
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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = null,
                Request = "GetAllUsers()"
            };
            AddHistory(history);
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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = id.ToString(),
                Request = $"GetUser({id})"
            };
            AddHistory(history);
            return readUser;
        }
        //DepartmentAccess Section
        //Pairs user to department in a company and assigns access.
        //Add DepartmentAccess
        public bool AddDepartmentAccess(CreateDepartmentAccess model)
        {
            ctx.DepartmentAccess.Add(new DepartmentAccess { DepartmentID = model.DepartmentID, CompanyID = _companyID, UserID = model.UserID, PermissionID = model.PermissionID, CreatedDateUTC = DateTimeOffset.UtcNow });
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = null,
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);

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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = id.ToString(),
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
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
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = id.ToString(),
                Request = $"DeleteDepartmentAccess({id})"
            };
            return AddHistory(history);
        }
        //List DepartmentAccess
        public IEnumerable<ReadDepartmentAccess> GetAllDepartmentAccess()
        {
            var query = ctx.DepartmentAccess.Where(e => e.CompanyID == _companyID).Select(f => new ReadDepartmentAccess
            {
                DepartmentID = f.DepartmentID,
                CompanyID = f.CompanyID,
                DepartmentName = f.Companies.CompanyName,
                UserID = f.UserID,
                PermissionID = f.PermissionID,
                CreatedDateUTC = f.CreatedDateUTC,
                ModifiedDateUTC = f.ModifiedDateUTC
            });
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = null,
                Request = $"GetAllDepartmentAccess()"
            };
            AddHistory(history);
            return query.ToList();
        }
        //Get DepartmentAccess
        public ReadDepartmentAccess GetDepartmentAccess(int id)
        {
            DepartmentAccess departmentAccess = ctx.DepartmentAccess.Single(e => e.CompanyID == _companyID && e.DepartmentID == id);
            ReadDepartmentAccess readDepartmentAccess = new ReadDepartmentAccess
            {
                DepartmentID = departmentAccess.DepartmentID,
                CompanyID = departmentAccess.CompanyID,
                DepartmentName = departmentAccess.Companies.CompanyName,
                UserID = departmentAccess.UserID,
                PermissionID = departmentAccess.PermissionID,
                CreatedDateUTC = departmentAccess.CreatedDateUTC,
                ModifiedDateUTC = departmentAccess.ModifiedDateUTC
            };
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = id.ToString(),
                Request = $"GetDepartmentAccess({id})"
            };
            AddHistory(history);
            return readDepartmentAccess;
        }
        //Populate History Table
        private bool AddHistory(CreateHistory model)
        {
            StackFrame stack = new StackFrame(1);
            ctx.History.Add(new History
            {
                CompanyID = model.CompanyID,
                UserID = model.UserID,
                Table = model.Table,
                Method = stack.GetMethod().Name,
                stringID = model.stringID,
                Request = model.Request,
                CreatedDateUTC = DateTimeOffset.UtcNow
            });
            return ctx.SaveChanges()>0;
        }
    }
}
