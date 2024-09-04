namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaqEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        FaqId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.FaqId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Faqs");
        }
    }
}
