using CRMData;
using CRMModels.Create;
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


        //Create Contact
        ////public bool CreateContact(CreateContact model)
        ////{
        ////    ctx.Contacts.Add(new Contact
        ////    {
        ////        FirstName = model.FirstName,
        ////        LastName = model.LastName,
        ////        PreferredName = model.PreferredName,
        ////        Email = model.Email,
        ////        CellPhone = model.CellPhone,
        ////        OwnerID = _userID.ToString(),
        ////    });
        ////    CreateHistory history = new CreateHistory
        ////    {
        ////        CompanyID = _companyID,
        ////        UserID = _userID.ToString(),
        ////        Table = "Contact",
        ////        stringID = null,
        ////        Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
        ////    };
        ////    return AddHistory(history);
        ////}
        //Update Contact
        //Delete Contact
        //Read Contact
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
                //departmentIDs.Add(item.DepartmentID);
            }
            //List<string> applicationUsers = new List<string>();
            //foreach (int item in departmentIDs)
            //{
            //    foreach (var user in ctx.Users)
            //    {
            //        if (user.DepartmentID == item)
            //        {
            //            applicationUsers.Add(user.Id);
            //        }
            //    }
            //}
            //List<ReadAllContact> readContacts = new List<ReadAllContact>();
            //int permissionID = 0;
            //string username;
            //foreach (var user in applicationUsers)
            //{
            //    permissionID = ctx.DepartmentAccess.FirstOrDefault(e => e.DepartmentID == ctx.Users.FirstOrDefault(f => f.Id == user).DepartmentID).PermissionID;
                
            //    foreach (var contact in ctx.Contacts)
            //    {
            //        username = dbx.Users.Single(e => e.Id == contact.OwnerID).UserName;
            //        if (user == contact.OwnerID)
            //        {
            //            readContacts.Add(new ReadAllContact
            //            {
            //                ContactID = contact.ContactID,
            //                FirstName = contact.FirstName,
            //                LastName = contact.LastName,
            //                PreferredName = contact.PreferredName,
            //                Email = contact.Email,
            //                CellPhone = contact.CellPhone,
            //                Username = username,
            //                CreatedDateUTC = contact.CreatedDateUTC,
            //                ModifiedDateUTC = contact.ModifiedDateUTC,
            //                PermissionID = permissionID                            
            //            });
            //        }
            //    }
            //    permissionID = 0;
            //    username = "";
            //}
            return readContacts;
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