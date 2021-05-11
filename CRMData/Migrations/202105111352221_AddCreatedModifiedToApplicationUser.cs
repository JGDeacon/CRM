namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedModifiedToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "CreatedDateUTC", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.ApplicationUser", "ModifiedDateUTC", c => c.DateTimeOffset(precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUser", "ModifiedDateUTC");
            DropColumn("dbo.ApplicationUser", "CreatedDateUTC");
        }
    }
}
