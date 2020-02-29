using PluralsightManager.Repositories.Contracts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories
{
    /// <summary>
    /// Implementing basic tables, views, procedures, functions, and transaction functions
    /// Select (Get, GetAll), Insert (Add), Delete, and Attach
    /// No Edit (Modify) function (can modify attached entity without function call)
    /// Executes database procedures or functions (Execute)
    /// Transaction functions (Commit)
    /// More functions can be added if needed
    /// </summary>
    /// <typeparam name="TEntity">Entity Framework table or view</typeparam>
    public partial class DatabaseRepository : IDatabaseRepository
    {
        #region Tables and Views functions

        /// <summary>
        /// Retrieve a specific entity
        /// </summary>
        public virtual TResult Get<TResult>(params object[] keyValues) where TResult : class
        {
            var entity = GetDbSet<TResult>().Find(keyValues);

            return entity;
        }

        /// <summary>
        /// Query all
        /// Set noTracking to true for selecting only (read-only queries)
        /// Set noTracking to false for insert, update, or delete after select
        /// </summary>
        public virtual IQueryable<TResult> GetAll<TResult>(bool noTracking = true) where TResult : class
        {
            var entityDbSet = GetDbSet<TResult>();

            if (noTracking)
                return entityDbSet.AsNoTracking();

            return entityDbSet;
        }

        public virtual TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            return GetDbSet<TEntity>().Add(entity);
        }

        /// <summary>
        /// Delete loaded (attached) or unloaded (Detached) entitiy
        /// No need to load object to delete it
        /// Create new object of TEntity and set the id then call Delete function
        /// </summary>
        /// <param name="entity">TEntity</param>
        /// <returns></returns>
        public virtual TEntity Delete<TEntity>(TEntity entity) where TEntity : class
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbContext.Entry(entity).State = EntityState.Deleted;
                return entity;
            }
            else
                return GetDbSet<TEntity>().Remove(entity);
        }

        public virtual TEntity Attach<TEntity>(TEntity entity) where TEntity : class
        {
            return GetDbSet<TEntity>().Attach(entity);
        }

        public virtual TEntity AttachIfNot<TEntity>(TEntity entity) where TEntity : class
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                return Attach(entity);

            return entity;
        }

        #endregion Tables and Views functions

        #region Transactions Functions

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public virtual int Commit()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.  The task result contains the number of objects written to the underlying database.</returns>
        public virtual Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion Transactions Functions

        #region Database Procedures and Functions

        /// <summary>
        /// Executes any function in the context
        /// use to call database procesdures and functions
        /// </summary>>
        /// <typeparam name="TResult">return function type</typeparam>
        /// <param name="functionName">context function name</param>
        /// <param name="parameters">context function parameters in same order</param>
        public virtual TResult Execute<TResult>(string functionName, params object[] parameters)
        {
            MethodInfo method = _dbContext.GetType().GetMethod(functionName);

            return (TResult)method.Invoke(_dbContext, parameters);
        }

        #endregion
    }
}
