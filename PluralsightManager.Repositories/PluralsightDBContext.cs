using PluralsightManager.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    public interface IDbContext
    {
        DbEntityEntry Entry(Object entity);
        IEnumerable<DbEntityValidationResult> GetValidationErrors();
        Int32 SaveChanges();
        Task<Int32> SaveChangesAsync();
        Task<Int32> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet Set(Type entityType);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }

    public class PluralsightDbContext : DbContext, IDbContext
    {
        public PluralsightDbContext()
            : base("name=PluralsightDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<CourseEntity>()
                .ToTable("Course");
        }
    }
}
