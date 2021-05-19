﻿using CRMData;
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
    public class ContactService
    {
        protected readonly ApplicationDbContext ctx = new ApplicationDbContext();
        private readonly Guid _userID;       
        private readonly int _companyID;       

        public ContactService(Guid userID)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ctx));
            _userID = userID;
            _companyID = ctx.Users.Single(e => e.Id.ToString() == userID.ToString()).CompanyID;           
        }

        //User Methods
        //Create Contact
        public bool CreateContact(CreateContact model)
        {
            ctx.Contacts.Add(new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PreferredName = model.PreferredName,
                Email = model.Email,
                CellPhone = model.CellPhone,
                OwnerID = _userID.ToString(),
            });
            CreateHistory history = new CreateHistory
            {
                CompanyID = _companyID,
                UserID = _userID.ToString(),
                Table = "Contact",
                stringID = null,
                Request = Newtonsoft.Json.JsonConvert.SerializeObject(model)
            };
            return AddHistory(history);
        }
        //Update Contact
        //Delete Contact
        //Read Contact
        public IEnumerable<ReadContact> GetAllUserContacts()
        {
            var query = ctx.Contacts.Where(e => e.OwnerID == _userID.ToString()).Select(f => new ReadContact
            {
                ContactID = f.ContactID,
                FirstName = f.FirstName,
                LastName = f.LastName,
                PreferredName = f.PreferredName,
                Email = f.Email,
                CellPhone = f.CellPhone,
                CreatedDateUTC = f.CreatedDateUTC,
                ModifiedDateUTC = f.ModifiedDateUTC
            });
            return query.ToList();
        }
        //Read All Contacts

        //Manager Methods
        //Create
        //Update Contact
        //Delete Contact
        //Read Contact
        //Read All Contacts

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
