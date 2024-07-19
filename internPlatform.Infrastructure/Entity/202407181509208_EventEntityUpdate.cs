namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventEntityUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EventLocation_Latitude", c => c.Double());
            AlterColumn("dbo.Events", "EventLocation_Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EventLocation_Longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Events", "EventLocation_Latitude", c => c.Double(nullable: false));
        }
    }
}
