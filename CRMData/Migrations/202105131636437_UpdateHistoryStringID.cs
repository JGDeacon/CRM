namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHistoryStringID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.History", "stringID", c => c.String());
            DropColumn("dbo.History", "IntID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.History", "IntID", c => c.Int());
            DropColumn("dbo.History", "stringID");
        }
    }
}
