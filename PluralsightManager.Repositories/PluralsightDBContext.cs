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
                .ToTable("Course");

            modelBuilder
                .Entity<ModuleEntity>()
                .ToTable("Module");
        }
    }
}
