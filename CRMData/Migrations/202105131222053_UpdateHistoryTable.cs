namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHistoryTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.History", "Method", c => c.String());
            AddColumn("dbo.History", "Request", c => c.String());
            DropColumn("dbo.History", "Change");
        }
        
        public override void Down()
        {
            AddColumn("dbo.History", "Change", c => c.String());
            DropColumn("dbo.History", "Request");
            DropColumn("dbo.History", "Method");
        }
    }
}
