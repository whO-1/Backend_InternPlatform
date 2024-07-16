namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEventCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EntryTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        SpecialGuests = c.String(),
                        AgeGroupId = c.Int(),
                        EntryTypeId = c.Int(),
                        AuthorId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupId)
                .ForeignKey("dbo.EntryTypes", t => t.EntryTypeId)
                .Index(t => t.AgeGroupId)
                .Index(t => t.EntryTypeId);
            
            CreateTable(
                "dbo.EventCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkTitle = c.String(nullable: false, maxLength: 20),
                        LinkUrl = c.String(),
                        DisplayOrder = c.Int(nullable: false),
                        HeadId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Links", t => t.HeadId)
                .Index(t => t.HeadId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "HeadId", "dbo.Links");
            DropForeignKey("dbo.EventCategories", "EventId", "dbo.Events");
            DropForeignKey("dbo.EventCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Events", "EntryTypeId", "dbo.EntryTypes");
            DropForeignKey("dbo.Events", "AgeGroupId", "dbo.AgeGroups");
            DropIndex("dbo.Links", new[] { "HeadId" });
            DropIndex("dbo.EventCategories", new[] { "CategoryId" });
            DropIndex("dbo.EventCategories", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "EntryTypeId" });
            DropIndex("dbo.Events", new[] { "AgeGroupId" });
            DropTable("dbo.Links");
            DropTable("dbo.EventCategories");
            DropTable("dbo.Events");
            DropTable("dbo.EntryTypes");
            DropTable("dbo.Categories");
            DropTable("dbo.AgeGroups");
        }
    }
}
