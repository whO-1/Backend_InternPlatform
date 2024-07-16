using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection.Emit;
using internPlatform.Domain.Entities;
using Microsoft.Owin.BuilderProperties;

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

        public DbSet<Event> Events { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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


            modelBuilder.ComplexType<Location>();
            modelBuilder.ComplexType<TimeStamp>();

            base.OnModelCreating(modelBuilder);
        }
    }
}