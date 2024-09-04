using internPlatform.Domain.Entities;
using System.Data.Entity;

namespace internPlatform.Infrastructure.Data
{
    public class ProjectDBContext : DbContext
    {
        public ProjectDBContext() : base("DefaultConnection")
        {
        }
        public ProjectDBContext(string connectionString) : base(connectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<EntryType> EntryTypes { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faq>()
                .HasKey(e => e.FaqId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithRequired(e => e.User);

            modelBuilder.Entity<Link>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Category>()
               .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Link>()
                .HasOptional(l => l.HeadLink)
                .WithMany()
                .HasForeignKey(l => l.HeadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasOptional(e => e.Age)
                .WithMany()
                .HasForeignKey(e => e.AgeGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Event>()
                .HasOptional(e => e.Entry)
                .WithMany()
                .HasForeignKey(e => e.EntryTypeId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Event>()
                .HasMany(e => e.Categories)
                .WithMany(e => e.Events);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Images)
                .WithOptional(e => e.Event);


            modelBuilder.ComplexType<Location>();
            modelBuilder.ComplexType<TimeStamp>();


            base.OnModelCreating(modelBuilder);
        }
    }
}