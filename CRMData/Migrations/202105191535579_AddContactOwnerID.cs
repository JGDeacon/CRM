namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactOwnerID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "OwnerID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "OwnerID");
        }
    }
}
