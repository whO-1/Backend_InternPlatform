namespace internPlatform.Infrastructure.Entity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTablesAndSeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeGroups",
                c => new
                    {
                        AgeGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AgeGroupId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(),
                        SpecialGuests = c.String(),
                        AgeGroupId = c.Int(),
                        EntryTypeId = c.Int(),
                        Author = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        TimeStamp_CreatedDate = c.DateTime(nullable: false),
                        TimeStamp_UpdateDate = c.DateTime(nullable: false),
                        EventLocation_LocationAddress = c.String(),
                        EventLocation_Latitude = c.Double(),
                        EventLocation_Longitude = c.Double(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        User_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.AgeGroups", t => t.AgeGroupId)
                .ForeignKey("dbo.EntryTypes", t => t.EntryTypeId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.AgeGroupId)
                .Index(t => t.EntryTypeId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.EntryTypes",
                c => new
                    {
                        EntryTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntryTypeId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsTemp = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        TimeStamp_CreatedDate = c.DateTime(nullable: false),
                        TimeStamp_UpdateDate = c.DateTime(nullable: false),
                        Event_EventId = c.Int(),
                    })
                .PrimaryKey(t => t.ImageId)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .Index(t => t.Event_EventId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        Name = c.String(),
                        TimeStamp_CreatedDate = c.DateTime(nullable: false),
                        TimeStamp_UpdateDate = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Image_ImageId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Images", t => t.Image_ImageId)
                .Index(t => t.Image_ImageId);
            
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
            
            CreateTable(
                "dbo.EventCategories",
                c => new
                    {
                        Event_EventId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Event_EventId, t.Category_CategoryId })
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .Index(t => t.Event_EventId)
                .Index(t => t.Category_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "HeadId", "dbo.Links");
            DropForeignKey("dbo.Users", "Image_ImageId", "dbo.Images");
            DropForeignKey("dbo.Events", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "EntryTypeId", "dbo.EntryTypes");
            DropForeignKey("dbo.EventCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.EventCategories", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "AgeGroupId", "dbo.AgeGroups");
            DropIndex("dbo.EventCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.EventCategories", new[] { "Event_EventId" });
            DropIndex("dbo.Links", new[] { "HeadId" });
            DropIndex("dbo.Users", new[] { "Image_ImageId" });
            DropIndex("dbo.Images", new[] { "Event_EventId" });
            DropIndex("dbo.Events", new[] { "User_UserId" });
            DropIndex("dbo.Events", new[] { "EntryTypeId" });
            DropIndex("dbo.Events", new[] { "AgeGroupId" });
            DropTable("dbo.EventCategories");
            DropTable("dbo.Links");
            DropTable("dbo.Users");
            DropTable("dbo.Images");
            DropTable("dbo.EntryTypes");
            DropTable("dbo.Events");
            DropTable("dbo.Categories");
            DropTable("dbo.AgeGroups");
        }
    }
}
