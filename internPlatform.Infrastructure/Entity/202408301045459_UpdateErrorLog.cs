namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateErrorLog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ErrorLogs", "Timestamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ErrorLogs", "Timestamp", c => c.DateTime(nullable: false));
        }
    }
}
