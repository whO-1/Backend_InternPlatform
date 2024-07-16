namespace internPlatform.Infrastructure.Entity
{
    using internPlatform.Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<internPlatform.Infrastructure.Data.ProjectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Entity";
        }

        protected override void Seed(internPlatform.Infrastructure.Data.ProjectDBContext context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryId,
                new Category { CategoryId = 1, Name = "Art", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "Music", DisplayOrder = 2 },
                new Category { CategoryId = 3, Name = "Cultural", DisplayOrder = 3 },
                new Category { CategoryId = 4, Name = "Outside", DisplayOrder = 4 },
                new Category { CategoryId = 5, Name = "Inside", DisplayOrder = 5 },
                new Category { CategoryId = 6, Name = "Sport", DisplayOrder = 6 },
                new Category { CategoryId = 7, Name = "Flashmob", DisplayOrder = 7 },
                new Category { CategoryId = 8, Name = "In Nature", DisplayOrder = 8 },
                new Category { CategoryId = 9, Name = "Exposition", DisplayOrder = 9 },
                new Category { CategoryId = 10, Name = "Political", DisplayOrder = 10 }
            );

            context.AgeGroups.AddOrUpdate(c => c.Id,
                new AgeGroup { Id = 1, Name = "Everyone", DisplayOrder = 1 },
                new AgeGroup { Id = 2, Name = "Over 18", DisplayOrder = 2 },
                new AgeGroup { Id = 3, Name = "For kids", DisplayOrder = 3 }
            );

            context.EntryTypes.AddOrUpdate(c => c.Id,
                new EntryType { Id = 1, Name = "Free", DisplayOrder = 1 },
                new EntryType { Id = 2, Name = "Paid", DisplayOrder = 2 }
            );

            context.Links.AddOrUpdate(c => c.Id,
                new Link { Id = 1, HeadId = null, LinkTitle = "New events", LinkUrl = "/feed?filter=new", DisplayOrder = 1 },
                new Link { Id = 2, HeadId = null, LinkTitle = "Around You", LinkUrl = "/feed?filter=around", DisplayOrder = 2 },
                new Link { Id = 3, HeadId = null, LinkTitle ="All Events", LinkUrl = "/feed", DisplayOrder = 3 },
                new Link { Id = 4, HeadId = 1, LinkTitle = "This Weekend", LinkUrl = "/feed?filter=thisweekend", DisplayOrder =1 },
                new Link { Id = 5, HeadId = 1, LinkTitle = "Today", LinkUrl = "/feed?filter=thisweekend", DisplayOrder = 2 },
                new Link { Id = 6, HeadId = 1, LinkTitle = "Tommorow", LinkUrl = "/feed?filter=thisweekend", DisplayOrder = 3 }
                
            );
        }
    }
}
