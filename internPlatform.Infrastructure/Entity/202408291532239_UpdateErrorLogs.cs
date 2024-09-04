namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateErrorLogs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ErrorLogs", "LineNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ErrorLogs", "LineNumber", c => c.String());
        }
    }
}
