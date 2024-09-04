namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddErrorLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ErrorLogs",
                c => new
                    {
                        ErrorLogId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Level = c.String(),
                        Logger = c.String(),
                        Message = c.String(),
                        Exception = c.String(),
                        CallSite = c.String(),
                        LineNumber = c.String(),
                    })
                .PrimaryKey(t => t.ErrorLogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ErrorLogs");
        }
    }
}
