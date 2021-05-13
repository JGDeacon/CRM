namespace CRMData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHistoryTableUserID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.History", "UserID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.History", "UserID", c => c.Int(nullable: false));
        }
    }
}
