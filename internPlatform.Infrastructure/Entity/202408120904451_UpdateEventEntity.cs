namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEventEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Likes", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Views");
            DropColumn("dbo.Events", "Likes");
        }
    }
}
