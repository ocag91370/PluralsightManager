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
        public void Dispose()
        {
            //_dbContext.Dispose();
        }
    }
}
