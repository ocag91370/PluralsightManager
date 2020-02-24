using System.Data.Entity;

namespace PluralsightManager.Repositories
{
    public class PluralsightDBContext : DbContext
    {
        public PluralsightDBContext()
            : base("name=PluralsightDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CourseEntity>()
                .ToTable("Course");
        }
    }
}
