using PluralsightManager.Repositories.Contracts;
using PluralsightManager.Repositories.Entities;
using System.Data.Entity;

namespace PluralsightManager.Repositories
{

    public class PluralsightDbContext : DbContext, IDbContext
    {
        public PluralsightDbContext()
            : base("name=PluralsightDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CourseEntity>()
                .ToTable("Course")
                .HasMany(t => t.Modules);

            modelBuilder
                .Entity<ModuleEntity>()
                .ToTable("Module")
                .HasMany(t => t.Clips);

            modelBuilder
                .Entity<ClipEntity>()
                .ToTable("Clip")
                .HasMany(t => t.Transcripts);

            modelBuilder
                .Entity<ClipTranscriptEntity>()
                .ToTable("ClipTranscript");
        }
    }
}
