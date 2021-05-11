namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyID = c.Guid(nullable: false),
                        CompanyName = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LogoURL = c.String(nullable: false),
                        ContactPerson = c.String(nullable: false, maxLength: 50),
                        StreetAddress = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        StateProvince = c.String(nullable: false, maxLength: 50),
                        Zip = c.String(nullable: false, maxLength: 15),
                        Country = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 25),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.CompanyID);
            
            CreateTable(
                "dbo.ContactList",
                c => new
                    {
                        ContactListID = c.Int(nullable: false, identity: true),
                        ContactID = c.Guid(nullable: false),
                        EndUserID = c.String(maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ContactListID)
                .ForeignKey("dbo.ApplicationUser", t => t.EndUserID)
                .ForeignKey("dbo.Contact", t => t.ContactID, cascadeDelete: true)
                .Index(t => t.ContactID)
                .Index(t => t.EndUserID);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactID = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        PreferredName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        CellPhone = c.String(nullable: false, maxLength: 50),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ContactID);
            
            CreateTable(
                "dbo.ContactMethodCredentials",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContactMethodID = c.Int(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        UserID = c.Guid(),
                        ConnectionString = c.String(),
                        Port = c.Int(),
                        Username = c.String(),
                        Password = c.String(),
                        URL = c.String(),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.ContactMethods", t => t.ContactMethodID, cascadeDelete: true)
                .Index(t => t.ContactMethodID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.ContactMethods",
                c => new
                    {
                        ContactMethodID = c.Int(nullable: false, identity: true),
                        ContactMethodName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactMethodID);
            
            CreateTable(
                "dbo.DepartmentAccess",
                c => new
                    {
                        DepartmentID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        PermissionID = c.Int(nullable: false),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => new { t.DepartmentID, t.CompanyID })
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        DepartmentName = c.String(nullable: false, maxLength: 50),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            CreateTable(
                "dbo.History",
                c => new
                    {
                        HistoryID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        Table = c.String(),
                        GuidID = c.Guid(),
                        IntID = c.Int(),
                        Change = c.String(),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.HistoryID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        PermissionID = c.Int(nullable: false, identity: true),
                        Access = c.String(),
                    })
                .PrimaryKey(t => t.PermissionID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Templates",
                c => new
                    {
                        TemplateID = c.Guid(nullable: false),
                        CompanyID = c.Guid(nullable: false),
                        UserID = c.Guid(),
                        ContactMethodID = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        PreviewLinkGuid = c.Guid(),
                        IsPublic = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ApprovedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ApprovedBy = c.String(maxLength: 128),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.TemplateID)
                .ForeignKey("dbo.ApplicationUser", t => t.ApprovedBy)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID)
                .Index(t => t.ApprovedBy);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        ContactListID = c.String(),
                        WorkflowTriggerID = c.Int(nullable: false),
                        ContactMethodID = c.Int(nullable: false),
                        Result = c.String(),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateTable(
                "dbo.WorkflowContactList",
                c => new
                    {
                        ContactListID = c.Int(nullable: false),
                        WorkflowID = c.Guid(nullable: false),
                        IsSubscribed = c.Boolean(nullable: false),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => new { t.ContactListID, t.WorkflowID })
                .ForeignKey("dbo.ContactList", t => t.ContactListID, cascadeDelete: true)
                .ForeignKey("dbo.Workflows", t => t.WorkflowID, cascadeDelete: true)
                .Index(t => t.ContactListID)
                .Index(t => t.WorkflowID);
            
            CreateTable(
                "dbo.Workflows",
                c => new
                    {
                        WorkflowID = c.Guid(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        ApprovedDate = c.DateTimeOffset(precision: 7),
                        ApprovedBy = c.Guid(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.WorkflowID);
            
            CreateTable(
                "dbo.WorkflowTriggers",
                c => new
                    {
                        WorkflowTriggerID = c.Int(nullable: false, identity: true),
                        WorkflowTriggerName = c.String(),
                        WorkflowID = c.Guid(nullable: false),
                        TemplateID = c.Guid(nullable: false),
                        ContactMethodID = c.Int(nullable: false),
                        TriggerLogic = c.String(),
                        CreatedBy = c.String(maxLength: 128),
                        CreatedDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedDateUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.WorkflowTriggerID)
                .ForeignKey("dbo.ApplicationUser", t => t.CreatedBy)
                .ForeignKey("dbo.Templates", t => t.TemplateID, cascadeDelete: true)
                .ForeignKey("dbo.Workflows", t => t.WorkflowID, cascadeDelete: true)
                .Index(t => t.WorkflowID)
                .Index(t => t.TemplateID)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkflowTriggers", "WorkflowID", "dbo.Workflows");
            DropForeignKey("dbo.WorkflowTriggers", "TemplateID", "dbo.Templates");
            DropForeignKey("dbo.WorkflowTriggers", "CreatedBy", "dbo.ApplicationUser");
            DropForeignKey("dbo.WorkflowContactList", "WorkflowID", "dbo.Workflows");
            DropForeignKey("dbo.WorkflowContactList", "ContactListID", "dbo.ContactList");
            DropForeignKey("dbo.Templates", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Templates", "ApprovedBy", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.DepartmentAccess", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.DepartmentAccess", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.ContactMethodCredentials", "ContactMethodID", "dbo.ContactMethods");
            DropForeignKey("dbo.ContactMethodCredentials", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.ContactList", "ContactID", "dbo.Contact");
            DropForeignKey("dbo.ContactList", "EndUserID", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.WorkflowTriggers", new[] { "CreatedBy" });
            DropIndex("dbo.WorkflowTriggers", new[] { "TemplateID" });
            DropIndex("dbo.WorkflowTriggers", new[] { "WorkflowID" });
            DropIndex("dbo.WorkflowContactList", new[] { "WorkflowID" });
            DropIndex("dbo.WorkflowContactList", new[] { "ContactListID" });
            DropIndex("dbo.Templates", new[] { "ApprovedBy" });
            DropIndex("dbo.Templates", new[] { "CompanyID" });
            DropIndex("dbo.DepartmentAccess", new[] { "CompanyID" });
            DropIndex("dbo.DepartmentAccess", new[] { "DepartmentID" });
            DropIndex("dbo.ContactMethodCredentials", new[] { "CompanyID" });
            DropIndex("dbo.ContactMethodCredentials", new[] { "ContactMethodID" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ContactList", new[] { "EndUserID" });
            DropIndex("dbo.ContactList", new[] { "ContactID" });
            DropTable("dbo.WorkflowTriggers");
            DropTable("dbo.Workflows");
            DropTable("dbo.WorkflowContactList");
            DropTable("dbo.Transactions");
            DropTable("dbo.Templates");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.Permissions");
            DropTable("dbo.History");
            DropTable("dbo.Departments");
            DropTable("dbo.DepartmentAccess");
            DropTable("dbo.ContactMethods");
            DropTable("dbo.ContactMethodCredentials");
            DropTable("dbo.Contact");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.ContactList");
            DropTable("dbo.Companies");
        }
    }
}
