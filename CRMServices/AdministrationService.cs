using CRMData;
using CRMModels.Create;
using CRMModels.Edit;
using CRMModels.Read;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
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
        private readonly string _role;
        private readonly int _companyID;
        static HistoryService historyService = new HistoryService();

        public AdministrationService(Guid userID)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            _userID = userID;
            _companyID = ctx.Users.Single(e => e.Id.ToString() == userID.ToString()).CompanyID;            
            string roles = UserManager.GetRoles(userID.ToString()).FirstOrDefault();
            _role = roles;
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
            });
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
            if (ctx.Departments.Single(e => e.DepartmentID == model.DepartmentID).CompanyID != _companyID)
            {
                //Sanataize Password in Model
                model.Password = "";
                model.ConfirmPassword = "";
                CreateHistory historyFail = new CreateHistory
                {
                    CompanyID = _companyID,
                    UserID = _userID.ToString(),
                    Table = "Users",
                    stringID = null,
                    Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
                };
                AddHistory(historyFail);
                return false;
            }
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser newUser = ctx.Users.Add(new ApplicationUser
            {
                CompanyID = _companyID,
                DepartmentID = model.DepartmentID,
                UserName = model.Username,
                Email = model.Email,
                PasswordHash = ph.HashPassword(model.Password),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserNumber = ctx.Users.Count()+1,
                CreatedDateUTC = DateTimeOffset.UtcNow
            });
            ctx.SaveChanges();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            string role = GetAllRoles().Single(e => e.RoleID == model.RoleID).RoleName;
            UserManager.AddToRole(newUser.Id, role);
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
        public bool EditUser(Guid id, EditUser model)
        {
            PasswordHasher ph = new PasswordHasher();
            ApplicationUser user = ctx.Users.Find(id.ToString());
            if (user == null)
            {
                return false;
            }
            user.CompanyID = _companyID;
            user.DepartmentID = model.DepartmentID;
            user.UserName = model.Username;
            user.Email = model.Email;
            user.LockoutEnabled = model.IsLocked;
            user.PasswordHash = ph.HashPassword(model.Password);
            user.SecurityStamp = Guid.NewGuid().ToString();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            string role = GetAllRoles().Single(e => e.RoleID == model.RoleID).RoleName;
            string removeRole = UserManager.GetRoles(model.UserID.ToString()).Single().ToString();
            UserManager.RemoveFromRole(model.UserID.ToString(), removeRole);
            UserManager.AddToRole(model.UserID.ToString(), role);
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
            List<ApplicationUser> applicationUsers = ctx.Users.Where(e => e.CompanyID == _companyID).ToList();
            List<ReadUser> readUsers = new List<ReadUser>();
            string department;
            foreach (ApplicationUser user in applicationUsers)
            {
                if (ctx.Departments.FirstOrDefault(e => e.DepartmentID == user.DepartmentID) == null)
                {
                    department = "No Department";
                }
                else
                {
                    department = ctx.Departments.FirstOrDefault(e => e.DepartmentID == user.DepartmentID).DepartmentName;
                }
                readUsers.Add(new ReadUser
                {
                    UserID = user.Id,
                    CompanyID = user.CompanyID,
                    DepartmentID = user.DepartmentID,
                    Department = department,
                    //RoleID = ctx.Roles.FirstOrDefault(e => e.Id ==  f.Id).Name,
                    RoleID = GetRoleID(user.Id),
                    //RoleName = _role,
                    RoleName = GetRoleName(user.Id),
                    Username = user.UserName,
                    Email = user.Email,
                    IsLocked = user.LockoutEnabled,
                    CreatedDateUTC = user.CreatedDateUTC
                });
            }

            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Users",
                stringID = null,
                Request = "GetAllUsers()"
            };
            AddHistory(history);
            return readUsers;
        }
             

        //Get User
        public ReadUser GetUser(Guid id)
        {
            ApplicationUser user = ctx.Users.Find(id.ToString());
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
        public EditUser GetUserEdit(Guid id)
        {
            ApplicationUser user = ctx.Users.Find(id.ToString());
            if (user == null) //Check for a Null User
            {
                return null;
            }
            if (user.CompanyID != _companyID) //Check that user is in the same company as the requesting user.
            {
                return null;
            }
            string roleName = GetRoleName(user.Id);
            string departmentName = ctx.Departments.Single(e => e.DepartmentID == user.DepartmentID).DepartmentName;
            EditUser editUser = new EditUser
            {
                UserID = Guid.Parse(user.Id),
                RoleName = roleName,
                DepartmentID = user.DepartmentID,
                DepartmentName = departmentName,
                Username = user.UserName,
                Email = user.Email,
                IsLocked = user.LockoutEnabled,
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
            return editUser;
        }
        //DepartmentAccess Section
        //Pairs user to department in a company and assigns access.
        //Add DepartmentAccess
        public bool AddDepartmentAccess(CreateDepartmentAccess model)
        {
            //var projectName = formcollection["UserID"];
            string uID = ctx.Users.Single(e => e.UserNumber == model.UserID).Id;
            ctx.DepartmentAccess.Add(new DepartmentAccess 
            {
                DepartmentID = model.DepartmentID, 
                CompanyID = _companyID, 
                UserID = uID, 
                PermissionID = model.PermissionID, 
                CreatedDateUTC = DateTimeOffset.UtcNow });
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
        public bool EditDepartmentAccess(EditDepartmentAccess model)
        {
            DepartmentAccess departmentAccess = ctx.DepartmentAccess.Find(model.DepartmentAccessID);
            if (departmentAccess == null)
            {
                return false;
            }
            if (departmentAccess.CompanyID != _companyID) //Check that user is in the same company as the requesting user.
            {
                return false;
            }
            string uID = ctx.Users.Single(e => e.UserNumber == model.UserID).Id;
            departmentAccess.DepartmentID = model.DepartmentID;
            departmentAccess.UserID = uID;
            departmentAccess.PermissionID = model.PermissionID;
            departmentAccess.ModifiedDateUTC = DateTimeOffset.UtcNow;
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "DepartmentAccess",
                stringID = model.DepartmentAccessID.ToString(),
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
                DepartmentAccessID = f.DepartmentAccessID,
                DepartmentID = f.DepartmentID,
                CompanyID = f.CompanyID,
                DepartmentName = ctx.Departments.FirstOrDefault(e => e.DepartmentID == f.DepartmentID).DepartmentName,
                UserID = ctx.Users.FirstOrDefault(e => e.Id == f.UserID).UserName,
                PermissionID = f.PermissionID,
                Access = ctx.Permissions.FirstOrDefault(e => e.PermissionID == f.PermissionID).Access,
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
        public EditDepartmentAccess GetDepartmentAccess(int id)
        {
            DepartmentAccess departmentAccess = ctx.DepartmentAccess.Single(e => e.CompanyID == _companyID && e.DepartmentAccessID == id);
            EditDepartmentAccess readDepartmentAccess = new EditDepartmentAccess
            {
                DepartmentAccessID = departmentAccess.DepartmentAccessID,
                DepartmentID = departmentAccess.DepartmentID,
                DepartmentName = ctx.Departments.Single(e => e.DepartmentID == departmentAccess.DepartmentID).DepartmentName,
                Username = ctx.Users.Single(e => e.Id == departmentAccess.UserID).UserName,
                //UserID = departmentAccess.UserID,
                PermissionID = departmentAccess.PermissionID,
                Access = ctx.Permissions.Single(e => e.PermissionID == departmentAccess.PermissionID).Access,
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
        //Helpers
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
            return ctx.SaveChanges() > 0;
        }
        //Get Roles
        public IEnumerable<ReadRoleList> GetAllRoles()
        {
            int count = 1;
            List<ReadRoleList> roleList = new List<ReadRoleList>();
            if (_role == "Administrator")
            {
                var adminQuery = ctx.Roles.Where(e => e.Name != null);
                foreach (var item in adminQuery)
                {
                    roleList.Add(new ReadRoleList { RoleID = count, RoleName = item.Name });
                    count++;
                }
                return roleList;
            }
            var query = ctx.Roles.Where(e => e.Name != "Administrator");
            foreach (var item in query)
            {
                roleList.Add(new ReadRoleList { RoleID = count, RoleName = item.Name });
                count++;
            }
            return roleList;
        }
        //Get Users for Department Access Assignment
        public IEnumerable<ReadDepartmentAccessUserList> GetDepartmentAccessUsers()
        {
            
            List<ReadDepartmentAccessUserList> userList = new List<ReadDepartmentAccessUserList>();            
            var query = ctx.Users.Where(e => e.CompanyID == _companyID);
            foreach (var item in query)
            {
                userList.Add(new ReadDepartmentAccessUserList { ID = item.UserNumber, Username = item.UserNumber + " " + item.UserName + " " + item.Id.ToString() });
            }
            return userList;
        }
        public IEnumerable<ReadPermissions> GetAllPermissions()
        {
            var query = ctx.Permissions.Where(e => e.PermissionID > 0).Select( f => new ReadPermissions { PermissionID = f.PermissionID, Access = f.Access });
            return query.ToList();
        }
        private string GetRoleName(string guid)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            string roles = UserManager.GetRoles(guid).FirstOrDefault();
            return roles;

        }

        private string GetRoleID(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            string roles = UserManager.GetRoles(id).FirstOrDefault();
            string roleID = ctx.Roles.Single(e => e.Name == roles).Id;
            return roleID;
        }
    }
}
