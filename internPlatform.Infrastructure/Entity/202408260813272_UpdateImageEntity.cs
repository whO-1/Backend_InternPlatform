namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateImageEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "IsMain", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "IsMain");
        }
    }
}
