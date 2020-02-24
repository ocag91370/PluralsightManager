using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PluralsightManager.Repositories.Contracts
{
    public interface IDbRepository : IDisposable
    {
        #region Tables and Views functions

        IQueryable<TResult> GetAll<TResult>(bool noTracking = true) where TResult : class;
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        TEntity Delete<TEntity>(TEntity entity) where TEntity : class;
        TEntity Attach<TEntity>(TEntity entity) where TEntity : class;
        TEntity AttachIfNot<TEntity>(TEntity entity) where TEntity : class;

        #endregion

        #region Transactions Functions

        int Commit();
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        #region Database Procedures and Functions

        TResult Execute<TResult>(string functionName, params object[] parameters);

        #endregion
    }
}
