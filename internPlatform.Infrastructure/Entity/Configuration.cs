namespace internPlatform.Infrastructure.Entity
{
    using internPlatform.Domain.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<internPlatform.Infrastructure.Data.ProjectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Entity";
        }

        protected override void Seed(internPlatform.Infrastructure.Data.ProjectDBContext context)
        {
            context.Faqs.AddOrUpdate(q => q.FaqId,
                    new Faq { FaqId = 1, Title = "How to register?", Description = "For the registration process you need you sign in with your email and verify account. " },
                    new Faq { FaqId = 2, Title = "How can I find an event?", Description = "You can search for your desired event or apply filters to get a list of recomandations." },
                    new Faq { FaqId = 3, Title = "Should I pay a tax for admin account?", Description = "No, creating an admin account is free of charge!" },
                    new Faq { FaqId = 4, Title = "Is EventPlanner platform a profit app ?", Description = "EventPlanner is a non profit app designed for all users around the world." }
            );

            var UserSuperAdmin = new User
            {
                UserId = "73586c3a-861a-4b8b-b391-2a3ffc8f05fc",
                Name = "SuperAdmin",
                Email = Constants.Constants.SuperAdminEmail,
                TimeStamp = new TimeStamp(),
                IsDeleted = false,
            };
            var UserAdmin = new User
            {
                UserId = "d7669648-5213-4cc8-a111-588a8a66592d",
                Name = "Admin",
                Email = Constants.Constants.AdminEmail,
                TimeStamp = new TimeStamp(),
                IsDeleted = false,
            };

            context.Users.AddOrUpdate(u => u.UserId,
                UserAdmin,
                UserSuperAdmin
            );
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

            context.AgeGroups.AddOrUpdate(c => c.AgeGroupId,
                new AgeGroup { AgeGroupId = 1, Name = "Everyone", DisplayOrder = 1 },
                new AgeGroup { AgeGroupId = 2, Name = "Over 18", DisplayOrder = 2 },
                new AgeGroup { AgeGroupId = 3, Name = "For kids", DisplayOrder = 3 }
            );

            context.EntryTypes.AddOrUpdate(c => c.EntryTypeId,
                new EntryType { EntryTypeId = 1, Name = "Free", DisplayOrder = 1 },
                new EntryType { EntryTypeId = 2, Name = "Paid", DisplayOrder = 2 }
            );

            context.Links.AddOrUpdate(c => c.Id,
                new Link { Id = 1, HeadId = null, LinkTitle = "New events", LinkUrl = "/feed?filter=new", DisplayOrder = 1 },
                new Link { Id = 2, HeadId = null, LinkTitle = "Around You", LinkUrl = "/feed?filter=around", DisplayOrder = 2 },
                new Link { Id = 3, HeadId = null, LinkTitle = "All Events", LinkUrl = "/feed", DisplayOrder = 3 },
                new Link { Id = 4, HeadId = 1, LinkTitle = "This Weekend", LinkUrl = "/feed?filter=thisweekend", DisplayOrder = 1 },
                new Link { Id = 5, HeadId = 1, LinkTitle = "Today", LinkUrl = "/feed?filter=thisweekend", DisplayOrder = 2 },
                new Link { Id = 6, HeadId = 1, LinkTitle = "Tommorow", LinkUrl = "/feed?filter=thisweekend", DisplayOrder = 3 }
            );

            context.Events.AddOrUpdate(e => e.EventId,
                new Event
                {
                    EventId = 1,
                    Title = "New Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 1,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 2,
                    Title = "Best Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 2,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 3,
                    Title = "Good Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 1,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 4,
                    Title = "Not bad Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 5,
                    Title = "Interesting Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 6,
                    Title = "Magic Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 7,
                    Title = "Magic Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 8,
                    Title = "Water Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 9,
                    Title = "Dark Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 10,
                    Title = "Light Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                },
                new Event
                {
                    EventId = 11,
                    Title = "Night Event",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. " +
                                    "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, " +
                                    "when an unknown printer took a galley of type and scrambled it to make a type specimen book. ",
                    SpecialGuests = "Guess Who",
                    AgeGroupId = 1,
                    EntryTypeId = 2,
                    Author = Constants.Constants.SuperAdminEmail,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today,
                    TimeStamp = new TimeStamp(),
                    User = UserSuperAdmin,
                    Likes = 0,
                    Views = 0,
                    IsDeleted = false,
                    IsBlocked = false,
                }
            );
            context.SaveChanges();
        }
    }
}
