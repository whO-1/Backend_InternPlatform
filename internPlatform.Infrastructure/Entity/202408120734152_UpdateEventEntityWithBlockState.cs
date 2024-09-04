namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEventEntityWithBlockState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "IsBlocked");
        }
    }
}
