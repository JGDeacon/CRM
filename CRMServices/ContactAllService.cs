using CRMData;
using CRMModels.Create;
using CRMModels.Edit;
using CRMModels.Read;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMServices
{
    public class ContactAllService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        protected readonly ApplicationDbContext dbx = new ApplicationDbContext();
        private readonly Guid _userID;
        private readonly int _companyID;

        public ContactAllService(Guid userID)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            _userID = userID;
            _companyID = ctx.Users.Single(e => e.Id.ToString() == userID.ToString()).CompanyID;
        }

        public IEnumerable<ReadAllContact> GetAllUserContacts()
        {
            List<DepartmentAccess> departmentAccess = ctx.DepartmentAccess.Where(e => e.UserID == _userID.ToString() && e.CompanyID == _companyID).ToList();
            List<int> departmentIDs = new List<int>();
            List<ReadAllContact> readContacts = new List<ReadAllContact>();
            foreach (DepartmentAccess item in departmentAccess)
            {
                foreach (ApplicationUser appUser in ctx.Users.Where(e => e.CompanyID == _companyID && e.DepartmentID == item.DepartmentID))
                {
                    foreach (var contact in dbx.Contacts)
                    {                        
                        if (appUser.Id == contact.OwnerID)
                        {
                            readContacts.Add(new ReadAllContact
                            {
                                ContactID = contact.ContactID,
                                FirstName = contact.FirstName,
                                LastName = contact.LastName,
                                PreferredName = contact.PreferredName,
                                Email = contact.Email,
                                CellPhone = contact.CellPhone,
                                Username = appUser.UserName,
                                CreatedDateUTC = contact.CreatedDateUTC,
                                ModifiedDateUTC = contact.ModifiedDateUTC,
                                PermissionID = item.PermissionID
                            });
                        }
                    }
                }                
            }
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Contact",
                stringID = null,
                Request = "GetAllUserContacts()"
            };
            AddHistory(history);
            return readContacts;
        }
        //Update Contact
        public EditContact GetContact(int id)
        {
            Contact contact = ctx.Contacts.Find(id);
            EditContact editContact = new EditContact
            {
                ContactID = contact.ContactID,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PreferredName = contact.PreferredName,
                Email = contact.Email,
                CellPhone = contact.CellPhone
            };
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Contacts",
                Method = $"GetContact(int id)",
                stringID = id.ToString(),
                Request = "GetContact(int id)",
            };
            AddHistory(history);
            return editContact;
        }
        public bool ContactEdit(EditContact model)
        {
            Contact contact = ctx.Contacts.Find(model.ContactID);
            if (contact == null)
            {
                return false;
            }
            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.PreferredName = model.PreferredName;
            contact.Email = model.Email;
            contact.CellPhone = model.CellPhone;
            contact.ModifiedDateUTC = DateTimeOffset.UtcNow;

            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Departments",
                stringID = model.ContactID.ToString(),
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
        }
        //Delete Contact
        public bool ContactDelete(int id)
        {
            Contact contact = ctx.Contacts.Find(id);
            if (contact == null)
            {
                return false;
            }
            ctx.Contacts.Remove(contact);
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Contacts",
                stringID = id.ToString(),
                Request = $"DeleteDepartment({id})"
            };
            return AddHistory(history);
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
    }
}