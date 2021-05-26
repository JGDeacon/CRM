namespace CRMData.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CRMData.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CRMData.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Create Admin Role
            string roleName = "Administrator";
            IdentityResult adminResult;

            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                adminResult = RoleManager.Create(new IdentityRole(roleName));
            }
            roleName = "Marketing Admin";
            IdentityResult marketingAdmin;
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists("Marketing Admin"))
            {
                marketingAdmin = RoleManager.Create(new IdentityRole(roleName));
            }
            roleName = "Marketing User";
            IdentityResult marketingUser;
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                marketingUser = RoleManager.Create(new IdentityRole(roleName));
            }
            roleName = "Analyst";
            IdentityResult analyst;
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                analyst = RoleManager.Create(new IdentityRole(roleName));
            }
            roleName = "Manager";            
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "End User";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "Developer";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "Contact";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "Vendor";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "Compliance";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            roleName = "Helpdesk";
            // Check to see if Role Exists, if not create it
            if (!RoleManager.RoleExists(roleName))
            {
                RoleManager.Create(new IdentityRole(roleName));
            }

            if (context.Companies.Count()==0)
            {
                //Permissions
                context.Permissions.Add(new Permissions { Access = "Read Only" });
                context.Permissions.Add(new Permissions { Access = "Edit" });
                //Companies
                context.Companies.Add(new Companies
                {
                    CompanyName = "BBQ",
                    IsActive = true,
                    LogoURL = "TBD",
                    ContactPerson = "BBQ Joe",
                    StreetAddress = "131 Main St",
                    City = "Fort Wayne",
                    StateProvince = "IN",
                    Zip = "46816",
                    Country = "USA",
                    Phone = "317-554-8898",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                }) ;
                context.Companies.Add(new Companies
                {
                    CompanyName = "Cookies",
                    IsActive = true,
                    LogoURL = "TBD",
                    ContactPerson = "Cookie Kid",
                    StreetAddress = "332 Sugar Lane",
                    City = "Churubusco",
                    StateProvince = "IN",
                    Zip = "56816",
                    Country = "USA",
                    Phone = "260-692-9920",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.SaveChanges();
                //Departments
                context.Departments.Add(new Departments
                {
                    CompanyID = 1,
                    DepartmentName = "East Coast",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.Departments.Add(new Departments
                {
                    CompanyID = 1,
                    DepartmentName = "West Coast",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.Departments.Add(new Departments
                {
                    CompanyID = 1,
                    DepartmentName = "Gulf Coast",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.Departments.Add(new Departments
                {
                    CompanyID = 2,
                    DepartmentName = "Indiana",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.Departments.Add(new Departments
                {
                    CompanyID = 2,
                    DepartmentName = "Michigan",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.Departments.Add(new Departments
                {
                    CompanyID = 2,
                    DepartmentName = "Ohio",
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.SaveChanges();
                PasswordHasher ph = new PasswordHasher();
                ApplicationUser ckAdmin = new ApplicationUser
                {
                    UserNumber=1,
                    UserName = "ckAdmin1@cookie.com",
                    Email = "ckAdmin1@cookie.com",
                    PasswordHash = ph.HashPassword("ckAdmin1@cookie.com"),
                    SecurityStamp = Guid.NewGuid().ToString(), 
                    CompanyID=2,
                    DepartmentID=1,
                    CreatedDateUTC = DateTimeOffset.UtcNow                    
                };
                context.Users.Add(ckAdmin);             
                ApplicationUser ckManager = new ApplicationUser
                {
                    UserNumber = 2,
                    UserName = "ckManager1@cookie.com",
                    Email = "ckManager1@cookie.com",
                    PasswordHash = ph.HashPassword("ckManager1@cookie.com"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CompanyID = 2,
                    DepartmentID = 2,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                };
                context.Users.Add(ckManager);
                ApplicationUser ckEndUser = new ApplicationUser
                {
                    UserNumber = 3,
                    UserName = "ckEndUser1@cookie.com",
                    Email = "ckEndUser1@cookie.com",
                    PasswordHash = ph.HashPassword("ckEndUser1@BBQ.com"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CompanyID = 2,
                    DepartmentID = 3,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                };
                context.Users.Add(ckEndUser);
                ApplicationUser BBQAdmin = new ApplicationUser
                {
                    UserNumber = 4,
                    UserName = "BBQAdmin1@BBQ.com",
                    Email = "BBQAdmin1@BBQ.com",
                    PasswordHash = ph.HashPassword("BBQAdmin1@BBQ.com"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CompanyID = 1,
                    DepartmentID = 4,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                };
                context.Users.Add(BBQAdmin);
                ApplicationUser BBQManager = new ApplicationUser
                {
                    UserNumber = 5,
                    UserName = "BBQManager1@BBQ.com",
                    Email = "BBQManager1@BBQ.com",
                    PasswordHash = ph.HashPassword("ckManager1@BBQ.com"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CompanyID = 1,
                    DepartmentID = 5,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                };
                context.Users.Add(BBQManager);
                ApplicationUser BBQVendor = new ApplicationUser
                {
                    UserNumber = 6,
                    UserName = "BBQVendor@BBQ.com",
                    Email = "BBQVendor@BBQ.com",
                    PasswordHash = ph.HashPassword("ckEndUser1@BBQ.com"),
                    SecurityStamp = Guid.NewGuid().ToString(),
                    CompanyID = 1,
                    DepartmentID = 6,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                };
                context.Users.Add(BBQVendor);
                context.SaveChanges();
                UserManager.AddToRole(ckAdmin.Id, "Administrator");
                UserManager.AddToRole(BBQAdmin.Id, "Administrator");
                UserManager.AddToRole(ckManager.Id, "Manager");
                UserManager.AddToRole(BBQManager.Id, "Manager");
                UserManager.AddToRole(ckEndUser.Id, "End User");
                UserManager.AddToRole(BBQVendor.Id, "Vendor");
                context.SaveChanges();
                //DepartmentAccess
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 6,
                    CompanyID = 1,
                    UserID = BBQAdmin.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 5,
                    CompanyID = 1,
                    UserID = BBQAdmin.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 4,
                    CompanyID = 1,
                    UserID = BBQAdmin.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 6,
                    CompanyID = 1,
                    UserID = BBQManager.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 5,
                    CompanyID = 1,
                    UserID = BBQManager.Id,
                    PermissionID = 2,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 3,
                    CompanyID = 2,
                    UserID = ckAdmin.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 2,
                    CompanyID = 2,
                    UserID = ckAdmin.Id,
                    PermissionID = 2,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 1,
                    CompanyID = 2,
                    UserID = ckAdmin.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 3,
                    CompanyID = 2,
                    UserID = ckManager.Id,
                    PermissionID = 1,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                context.DepartmentAccess.Add(new DepartmentAccess
                {
                    DepartmentID = 2,
                    CompanyID = 2,
                    UserID = ckManager.Id,
                    PermissionID = 2,
                    CreatedDateUTC = DateTimeOffset.UtcNow
                });
                //Contacts
                context.Contacts.Add(new Contact
                {
                    FirstName = "Tito",
                    LastName = "Jackson",
                    PreferredName = "Yes I'm related to Michael",
                    Email = "Tjax@hotmail.com",
                    CellPhone = "219.445.5546",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = BBQAdmin.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Son",
                    LastName = "Goku",
                    PreferredName = "The Best",
                    Email = "SSG@CapsoleCorp.com",
                    CellPhone = "616-884-9654",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = BBQManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Yamcha",
                    LastName = "Yamcha",
                    PreferredName = "Fodder",
                    Email = "ManDown@yahoo.com",
                    CellPhone = "965.455.4448",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = BBQManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Freeza",
                    LastName = "Freeza",
                    PreferredName = "Lord Freeza",
                    Email = "Freeza@msn.com",
                    CellPhone = "317.485.1546",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = BBQManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Dr.",
                    LastName = "Gero",
                    PreferredName = "Dr. Gero",
                    Email = "WhatsUpDoc@RRArmy.com",
                    CellPhone = "819.845.1245",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = BBQManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Tony",
                    LastName = "Stark",
                    PreferredName = "Iron Man",
                    Email = "TSizzle@avengers.com",
                    CellPhone = "219.445.5546",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckAdmin.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Steve",
                    LastName = "Rodgers",
                    PreferredName = "Capt. America",
                    Email = "SRodgers@army.gov",
                    CellPhone = "919.415.5886",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckAdmin.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Thor",
                    LastName = "Odinson",
                    PreferredName = "Point Break",
                    Email = "thor@avengers.com",
                    CellPhone = "319.435.9946",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Clint",
                    LastName = "Barton",
                    PreferredName = "Hawkeye",
                    Email = "bullseye@avengers.com",
                    CellPhone = "519.945.1246",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Natasha",
                    LastName = "Rominahv",
                    PreferredName = "Black Widow",
                    Email = "nat@avengers.com",
                    CellPhone = "212.884.5463",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Nick",
                    LastName = "Fury",
                    PreferredName = "Nick",
                    Email = "na@na.com",
                    CellPhone = "111.111.1111",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Sam",
                    LastName = "Wilson",
                    PreferredName = "Falcon",
                    Email = "falcon@avengers.com",
                    CellPhone = "515.665.7447",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckManager.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Bruce",
                    LastName = "Banner",
                    PreferredName = "Hulk",
                    Email = "smash@avengers.com",
                    CellPhone = "774.545.6644",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckEndUser.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    PreferredName = "Spider-man",
                    Email = "noobmaster69@gmail.com",
                    CellPhone = "214.845.9746",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckEndUser.Id
                });
                context.Contacts.Add(new Contact
                {
                    FirstName = "Wanda",
                    LastName = "Maximoff",
                    PreferredName = "Scarlet Witch",
                    Email = "wmaximoff@avengers.com",
                    CellPhone = "214.445.7757",
                    CreatedDateUTC = DateTimeOffset.UtcNow,
                    OwnerID = ckEndUser.Id
                });
                context.SaveChanges();

            }
        }
    }
}
