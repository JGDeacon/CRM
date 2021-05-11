using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using CRMData;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CRMData
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public Guid DepartmentID { get; set; }
        public Guid CompanyID { get; set; }
        public DateTimeOffset CreatedDateUTC { get; set; }
        public DateTimeOffset? ModifiedDateUTC { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactList> ContactLists { get; set; }
        public DbSet<ContactMethodCredentials> ContactMethodCredentials { get; set; }
        public DbSet<ContactMethods> ContactMethods { get; set; }
        public DbSet<DepartmentAccess> DepartmentAccess { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Templates> Templates { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<WorkflowContactList> WorkflowContactLists { get; set; }
        public DbSet<Workflows> Workflows { get; set; }
        public DbSet<WorkflowTriggers> WorkflowTriggers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new IdentityUserLoginConfiguration()).Add(new IdentityUserRoleConfiguration());

        }
    }
    public class IdentityUserLoginConfiguration : EntityTypeConfiguration<IdentityUserLogin>
    {
        public IdentityUserLoginConfiguration()
        {
            HasKey(iul => iul.UserId);
        }
    }
    public class IdentityUserRoleConfiguration : EntityTypeConfiguration<IdentityUserRole>
    {
        public IdentityUserRoleConfiguration()
        {
            HasKey(iur => iur.UserId);
        }
    }
}