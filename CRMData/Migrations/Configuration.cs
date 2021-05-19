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
        }
    }
}
