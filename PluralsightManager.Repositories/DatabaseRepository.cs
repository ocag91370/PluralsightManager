using PluralsightManager.Repositories.Contracts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    /// <summary>
    /// Implementing basic tables, views, procedures, functions, and transaction functions
    /// Select (GetAll), Insert (Add), Delete, and Attach
    /// No Edit (Modify) function (can modify attached entity without function call)
    /// Executes database procedures or functions (Execute)
    /// Transaction functions (Commit)
    /// More functions can be added if needed
    /// </summary>
    /// <typeparam name="TEntity">Entity Framework table or view</typeparam>
    public partial class DatabaseRepository : IDatabaseRepository
    {
        #region Members

        private readonly IDbContext _dbContext;

        #endregion

        #region Implementation

        /// <summary>
        /// Repository constructor 
        /// </summary>
        /// <param name="dbContext">Entity framework database context</param>
        public DatabaseRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        #endregion
    }
}
