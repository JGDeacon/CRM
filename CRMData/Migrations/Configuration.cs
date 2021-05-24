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

            if (!(context.Companies.Count()==0))
            {
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

            }
        }
    }
}
